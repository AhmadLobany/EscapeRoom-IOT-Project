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

private static Random random = new Random();
public static string RandomString(int length)
{
    const string chars = "123456789";
    return new string(Enumerable.Repeat(chars, length)
      .Select(s => s[random.Next(s.Length)]).ToArray());
}





public static async Task<IActionResult> Run(HttpRequestMessage req, TraceWriter log)
{
    // Section A: Get event data
    // dynamic body = await req.Content.ReadAsAsync<object>();
    
  
  
    // Section B: Create the SQL query based on event type
    string text;
      string new_string1 = RandomString(1);
      string new_string2 = RandomString(1);
       string new_string3 = RandomString(1);
       string new_string4 = RandomString(1);
     string new_string5 = RandomString(1);
     string new_string= 'S' + new_string1 + 'S'+ '1' + new_string2 + 'S' + new_string3 + 'S' + '1'+ new_string4 + 'S' + new_string5;   
    text = "UPDATE dbo.Cyphers " +
            "SET Puzzle1=@1 ;";
            
    // Log for debugging and information
    log.Info("Update:" + new_string);

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
                 cmd.Parameters.AddWithValue("@1",new_string);
                var rows = await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                // Ignore duplicate events...
            }
            catch (SqlException ex)
            {
                // but propagate other errors so that the Data Connector can retry later.
            
            }
        }
    }
    return (ActionResult)new OkObjectResult($"{new_string}");
}
