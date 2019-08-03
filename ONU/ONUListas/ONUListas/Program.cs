<<<<<<< HEAD
﻿using AutoMapper;
using System;
using System.IO;
using System.Net;
=======
﻿using AspNetCore.Http.Extensions;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
using System.Xml;
using System.Xml.Serialization;

namespace ONUListas
{
    class Program
    {
<<<<<<< HEAD
=======
        static MyConfig moduleSettings = new MyConfig();
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
        static void Main(string[] args)
        {
            Console.WriteLine("Downloading data from https://scsanctions.un.org/resources/xml/en/consolidated.xml");

            try
            {
<<<<<<< HEAD
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
=======
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();
                configuration.GetSection("AppSettings").Bind(moduleSettings);

                Ejecutar().Wait();

               
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

            }
            catch (Exception ex)
            {

                throw ex;
            }
          


        }

<<<<<<< HEAD
=======

        public async static Task<int> Ejecutar()
        {
            var data = GetData();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CONSOLIDATED_LIST, CONSOLIDATED_LISTM>().ForMember(d => d.Id, opt => opt.Ignore());
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

            Console.WriteLine("Inserting data to db.. Please wait");
            IMapper mapper = config.CreateMapper();

            CONSOLIDATED_LISTM CONSOLIDATED_LISTM = mapper.Map<CONSOLIDATED_LIST, CONSOLIDATED_LISTM>(data);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            string baseadress = moduleSettings.urlbase;
            HttpClient _client = new HttpClient();
            var resultlogin = await _client.PostAsJsonAsync(baseadress + "api/cuenta/login", new UserInfo { Email = moduleSettings.UserEmail, Password = moduleSettings.UserPassword });
            if (resultlogin.IsSuccessStatusCode)
            {
                string webtoken = await (resultlogin.Content.ReadAsStringAsync());
                UserToken _userToken = JsonConvert.DeserializeObject<UserToken>(webtoken);
                _client = new HttpClient();
                _client.Timeout = TimeSpan.FromMinutes(60);
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userToken.Token);
                var insertresult = await _client.PostAsJsonAsync(baseadress + "api/ONU/Insert", CONSOLIDATED_LISTM);
                if (insertresult.IsSuccessStatusCode)
                {

                }
            }

            return 1;

        }

>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
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
<<<<<<< HEAD
=======


    public class UserInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }

    public class MyConfig
    {
        public string urlbase { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string wsorbiteciahhrr { get; set; }
    }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
}
