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
                    cfg.CreateMap<CONSOLIDATED_LIST, CONSOLIDATED_LISTM>();
                cfg.CreateMap<INDIVIDUAL, INDIVIDUALM>()
                     .ForMember(d => d.tITLEField, opt => opt.MapFrom(src => String.Join(";", src.TITLE)))
                      .ForMember(d => d.dESIGNATIONField, opt => opt.MapFrom(src => String.Join(";", src.DESIGNATION)))
                       .ForMember(d => d.lAST_DAY_UPDATEDField, opt => opt.MapFrom(src => String.Join(";", src.LAST_DAY_UPDATED)))
                          .ForMember(d => d.nATIONALITYField, opt => opt.MapFrom(src => String.Join(";", src.NATIONALITY)));
                    cfg.CreateMap<LIST_TYPE, LIST_TYPEM>();

                    cfg.CreateMap<INDIVIDUAL_ALIAS, INDIVIDUAL_ALIASM>();
                    cfg.CreateMap<INDIVIDUAL_ADDRESS, INDIVIDUAL_ADDRESSM>();
                    cfg.CreateMap<INDIVIDUAL_DATE_OF_BIRTH, INDIVIDUAL_DATE_OF_BIRTHM>()
                    .ForMember(d => d.itemsField, opt => opt.MapFrom(src => String.Join(";", src.Items)))
                     .ForMember(d => d.itemsElementNameField, opt => opt.MapFrom(src => String.Join(";", src.ItemsElementName)));
                    
                    cfg.CreateMap<INDIVIDUAL_PLACE_OF_BIRTH, INDIVIDUAL_PLACE_OF_BIRTHM>();
                    cfg.CreateMap<INDIVIDUAL_DOCUMENT, INDIVIDUAL_DOCUMENTM>();

                    cfg.CreateMap<ENTITY, ENTITYM>()
                     .ForMember(d => d.lAST_DAY_UPDATEDField, opt => opt.MapFrom(src => String.Join(";", src.LAST_DAY_UPDATED)));
                    cfg.CreateMap<ENTITY_ADDRESS, ENTITY_ADDRESSM>();
                    cfg.CreateMap<ENTITY_ALIAS, ENTITY_ALIASM>();
                    cfg.CreateMap<INDIVIDUALS, INDIVIDUALSM>();
                    cfg.CreateMap<TITLE, TITLEM>()
                     .ForMember(d => d.vALUEField, opt => opt.MapFrom(src => String.Join(";", src.VALUE)));
                    ;

                    cfg.CreateMap<DESIGNATION, DESIGNATIONM>()
                     .ForMember(d => d.vALUEField, opt => opt.MapFrom(src => String.Join(";", src.VALUE))); 

                    cfg.CreateMap<NATIONALITY, NATIONALITYM>()
                    .ForMember(d => d.vALUEField, opt => opt.MapFrom(src => String.Join(";", src.VALUE)));
                    cfg.CreateMap<ENTITIES, ENTITIESM>();

                    cfg.CreateMap<LAST_DAY_UPDATED, LAST_DAY_UPDATEDM>()
                       .ForMember(d => d.vALUEField, opt => opt.MapFrom(src => String.Join(";", src.VALUE)));
                });

                IMapper mapper = config.CreateMapper();

                CONSOLIDATED_LISTM CONSOLIDATED_LISTM = mapper.Map<CONSOLIDATED_LIST, CONSOLIDATED_LISTM>(data);

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
