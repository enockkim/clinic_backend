using clinic.Models.clinic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC.Endo;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {       //[HttpPost("AddAppointment")]
        private ClinicService clinic;

        public LocationController(ClinicService clinicService)
        {
            clinic = clinicService;
        }


        [HttpGet("GetCounties")]
        public async Task<List<County>> GetCounties()
        {
            //AppointmentsComponent AppointmentsComponent = new AppointmentsComponent(clinic);
            try 
            {
                var counties = new List<County>();
                using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
                {
                    connection.Open();

                    //clear appointment
                    var query = $"SELECT id, county_name FROM counties";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        counties.Add(new County()
                        {
                            id = Convert.ToInt32(reader[0]),
                            county_name = reader[1].ToString()
                        });
                    }

                    connection.Close();
                }
                return counties;
            }
            catch (Exception ex)
            {
                Console.WriteLine("get counties: ",ex.ToString());
                return null;
            }
        }




        [HttpGet("GetSubCounties")]
        public async Task<List<subcounties>> GetSubCounties()
        {
            //AppointmentsComponent AppointmentsComponent = new AppointmentsComponent(clinic);
            try
            {
                var subcounties = new List<subcounties>();
                using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
                {
                    connection.Open();

                    //clear appointment
                    var query = $"Select subcountyId, subcounty, countyId from subcounty";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        subcounties.Add(new subcounties()
                        {
                            subcountyId = Convert.ToInt32(reader[0]),
                            subcountyName = reader[1].ToString(),
                            countyId = Convert.ToInt32(reader[2])
                        });
                    }

                    connection.Close();
                }
                return subcounties;
            }
            catch (Exception ex)
            {
                Console.WriteLine("get subcounties: ", ex.ToString());
                return null;
            }
        }

        [HttpGet("GetWards")]
        public async Task<List<wards>> GetWards()
        {
            //AppointmentsComponent AppointmentsComponent = new AppointmentsComponent(clinic);
            try
            {
                var wards = new List<wards>();
                using (var connection = new MySqlConnection("Server=202.182.120.224;Database=clinic;User ID=remote;Password=#3PqKZ$F3G=y9NSD;Connection Timeout=100"))
                {
                    connection.Open();

                    //clear appointment
                    var query = $"Select id, subcountyId, ward from wards";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        wards.Add(new wards()
                        {
                            id = Convert.ToInt32(reader[0]),
                            subcountyId = Convert.ToInt32(reader[1]),
                            ward = reader[2].ToString()
                        });
                    }

                    connection.Close();
                }
                return wards;
            }
            catch (Exception ex)
            {
                Console.WriteLine("get subcounties: ", ex.ToString());
                return null;
            }
        }
    }
}
