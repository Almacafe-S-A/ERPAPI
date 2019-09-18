using AspNetCore.Http.Extensions;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

namespace OFAC
{
    class Program
    {

        static MyConfig moduleSettings = new MyConfig();
        static  void Main(string[] args)
        {
            // Mapper.CreateMap<sdnList, sdnListM>();
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            // var res =  configuration.GetSection("AppSettings");
          
            configuration.GetSection("AppSettings").Bind(moduleSettings);

            try
            {
                // var res =  Task.Run( ()=>  Ejecutar());
                Ejecutar().Wait();
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }



        public async static Task<int> Ejecutar()
        {
            Console.WriteLine("Downloading data from http://www.treasury.gov/ofac/downloads/sdn.xml");
            var data = GetData();


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<sdnList, sdnListM>();
                cfg.CreateMap<sdnListPublshInformation, sdnListPublshInformationM>();
                cfg.CreateMap<sdnListSdnEntry, sdnListSdnEntryM>()
                    .ForMember(d => d.programList, opt => opt.MapFrom(src => String.Join(";", src.programList)));
                cfg.CreateMap<sdnListSdnEntryID, sdnListSdnEntryIDM>();
                cfg.CreateMap<sdnListSdnEntryAka, sdnListSdnEntryAkaM>();
                cfg.CreateMap<sdnListSdnEntryAddress, sdnListSdnEntryAddressM>();
                cfg.CreateMap<sdnListSdnEntryNationality, sdnListSdnEntryNationalityM>();
                cfg.CreateMap<sdnListSdnEntryCitizenship, sdnListSdnEntryCitizenshipM>();
                cfg.CreateMap<sdnListSdnEntryDateOfBirthItem, sdnListSdnEntryDateOfBirthItemM>();
                cfg.CreateMap<sdnListSdnEntryPlaceOfBirthItem, sdnListSdnEntryPlaceOfBirthItemM>();
                cfg.CreateMap<sdnListSdnEntryVesselInfo, sdnListSdnEntryVesselInfoM>();
            });

            IMapper mapper = config.CreateMapper();
            //var source = new Source();
            // var dest = mapper.Map<Source, Dest>(source);
            // **** convert data to entity framework models ****
            sdnListM sdnListM = mapper.Map<sdnList, sdnListM>(data);

            // **** setup db and insert data ****
            Console.WriteLine("Inserting data to db.. Please wait");
            HttpClient client = new HttpClient();

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
                //sdnListM _sdn = new sdnListM();
                //var delete = await _client.PostAsJsonAsync(baseadress + "api/OFAC/Delete", _sdn);
                //if (delete.IsSuccessStatusCode)
                //{
                //_client = new HttpClient();
                //_client.Timeout = TimeSpan.FromMinutes(60);
                //_client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userToken.Token);

                var insertresult = await _client.PostAsJsonAsync(baseadress + "api/OFAC/Insert", sdnListM);
                if (insertresult.IsSuccessStatusCode)
                {

                }
                // }
            }

            return 1;
        }

            public static sdnList GetData()
            {
                WebClient client = new WebClient();
                string res = client.DownloadString("http://www.treasury.gov/ofac/downloads/sdn.xml");
                File.WriteAllText(@"..\..\sdn.xml", res);

                sdnList sr = new sdnList();

                using (FileStream xmlStream = new FileStream(@"..\..\sdn.xml", FileMode.Open))
                {
                    using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(sdnList));
                        sdnList deserialized = serializer.Deserialize(xmlReader) as sdnList;
                        return deserialized;
                    }
                }


            }


        

    }

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


}
