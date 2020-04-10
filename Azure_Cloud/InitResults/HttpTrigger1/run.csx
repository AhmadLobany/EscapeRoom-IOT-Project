#r "Newtonsoft.Json"
#r "System.Configuration"
#r "System.Data"
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;



public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // Section A: Get event data
    // dynamic body = await req.Content.ReadAsAsync<object>();
    
  
  
    // Section B: Create the SQL query based on event type
    string text;
  
    text = "UPDATE dbo.Results " +
            "SET Puz1=0,Puz2=0,Puz3=0;";
            
    // Log for debugging and information
    log.Info("Update: ");

    // Section C: Connect to SQL database and 
    var str = Environment.GetEnvironmentVariable("sqlconnection");
    using (SqlConnection conn = new SqlConnection(str))
    {
        conn.Open();
        using (SqlCommand cmd = new SqlCommand(text, conn))
        {
            try
            {
                // Execute query
                var rows = await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                // Ignore duplicate events...
            }
            catch (SqlException ex)
            {
                // but propagate other errors so that the Data Connector can retry later.
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
    return req.CreateResponse(HttpStatusCode.OK);
}

