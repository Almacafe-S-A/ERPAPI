using AutoMapper;
using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace ONUListas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Downloading data from https://scsanctions.un.org/resources/xml/en/consolidated.xml");

            try
            {
                var data = GetData();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CONSOLIDATED_LIST, CONSOLIDATED_LISTM>().ForMember(d=>d.Id, opt=>opt.Ignore());
                 cfg.CreateMap<INDIVIDUAL, INDIVIDUALM>()
                     .ForMember(d => d.TITLE, opt => opt.MapFrom(src => String.Join(";", src.TITLE)))
                      .ForMember(d => d.DESIGNATION, opt => opt.MapFrom(src => String.Join(";", src.DESIGNATION)))
                       .ForMember(d => d.LAST_DAY_UPDATED, opt => opt.MapFrom(src => String.Join(";", src.LAST_DAY_UPDATED)))
                          .ForMember(d => d.NATIONALITY, opt => opt.MapFrom(src => String.Join(";", src.NATIONALITY)))
                          .ForMember(d => d.Id, opt => opt.Ignore())
                          ;
                    cfg.CreateMap<LIST_TYPE, LIST_TYPEM>().ForMember(d => d.Id, opt => opt.Ignore());

                    cfg.CreateMap<INDIVIDUAL_ALIAS, INDIVIDUAL_ALIASM>().ForMember(d => d.Id, opt => opt.Ignore());
                    cfg.CreateMap<INDIVIDUAL_ADDRESS, INDIVIDUAL_ADDRESSM>().ForMember(d => d.Id, opt => opt.Ignore());
                    cfg.CreateMap<INDIVIDUAL_DATE_OF_BIRTH, INDIVIDUAL_DATE_OF_BIRTHM>()
                    .ForMember(d => d.Items, opt => opt.MapFrom(src => String.Join(";", src.Items)))
                     .ForMember(d => d.ItemsElementName, opt => opt.MapFrom(src => String.Join(";", src.ItemsElementName)))
                     .ForMember(d => d.Id, opt => opt.Ignore())
                     ;
                    
                    cfg.CreateMap<INDIVIDUAL_PLACE_OF_BIRTH, INDIVIDUAL_PLACE_OF_BIRTHM>().ForMember(d => d.Id, opt => opt.Ignore());
                    cfg.CreateMap<INDIVIDUAL_DOCUMENT, INDIVIDUAL_DOCUMENTM>().ForMember(d => d.Id, opt => opt.Ignore());

                    cfg.CreateMap<ENTITY, ENTITYM>()
                     .ForMember(d => d.LAST_DAY_UPDATED, opt => opt.MapFrom(src => String.Join(";", src.LAST_DAY_UPDATED)))
                     .ForMember(d => d.Id, opt => opt.Ignore()); 

                    cfg.CreateMap<ENTITY_ADDRESS, ENTITY_ADDRESSM>().ForMember(d => d.Id, opt => opt.Ignore());
                    cfg.CreateMap<ENTITY_ALIAS, ENTITY_ALIASM>().ForMember(d => d.Id, opt => opt.Ignore());
                    cfg.CreateMap<INDIVIDUALS, INDIVIDUALSM>().ForMember(d => d.Id, opt => opt.Ignore());
                    cfg.CreateMap<TITLE, TITLEM>()
                     .ForMember(d => d.VALUE, opt => opt.MapFrom(src => String.Join(";", src.VALUE)))
                    .ForMember(d => d.Id, opt => opt.Ignore())
                    ;

                    cfg.CreateMap<DESIGNATION, DESIGNATIONM>()
                     .ForMember(d => d.VALUE, opt => opt.MapFrom(src => String.Join(";", src.VALUE)))
                     .ForMember(d => d.Id, opt => opt.Ignore());

                    cfg.CreateMap<NATIONALITY, NATIONALITYM>()
                    .ForMember(d => d.VALUE, opt => opt.MapFrom(src => String.Join(";", src.VALUE)))
                    .ForMember(d => d.Id, opt => opt.Ignore())
                    ;

                    cfg.CreateMap<ENTITIES, ENTITIESM>().ForMember(d => d.Id, opt => opt.Ignore());

                    cfg.CreateMap<LAST_DAY_UPDATED, LAST_DAY_UPDATEDM>()
                       .ForMember(d => d.VALUE, opt => opt.MapFrom(src => String.Join(";", src.VALUE)))
                       .ForMember(d => d.Id, opt => opt.Ignore());
                });

                IMapper mapper = config.CreateMapper();

                CONSOLIDATED_LISTM CONSOLIDATED_LISTM = mapper.Map<CONSOLIDATED_LIST, CONSOLIDATED_LISTM>(data);
                 mapper.ConfigurationProvider.AssertConfigurationIsValid();


                Console.WriteLine("Inserting data to db.. Please wait");

            }
            catch (Exception ex)
            {

                throw ex;
            }
          


        }

        public static CONSOLIDATED_LIST GetData()
        {
            WebClient client = new WebClient();
            string res = client.DownloadString("https://scsanctions.un.org/resources/xml/en/consolidated.xml");
            File.WriteAllText(@"..\..\consolidated.xml", res);

            CONSOLIDATED_LIST sr = new CONSOLIDATED_LIST();

            using (FileStream xmlStream = new FileStream(@"..\..\consolidated.xml", FileMode.Open))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CONSOLIDATED_LIST));
                    CONSOLIDATED_LIST deserialized = serializer.Deserialize(xmlReader) as CONSOLIDATED_LIST;
                    return deserialized;
                }
            }


        }


    }
}
