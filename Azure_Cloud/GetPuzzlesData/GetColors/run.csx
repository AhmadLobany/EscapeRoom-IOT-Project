



#r "Newtonsoft.Json"
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
public static async Task<IActionResult> Run(HttpRequest req, TraceWriter log)
{


    var jsonResult = new StringBuilder();


    var str = Environment.GetEnvironmentVariable("sqlconnection");
    using (SqlConnection conn = new SqlConnection(str) )
    {
        conn.Open();
        var sqlQuery = "SELECT Puzzle2 FROM dbo.Cyphers;";
        using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
        {
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                if (reader.HasRows){
                    while (reader.Read()){
                         jsonResult.Append(reader.GetValue(0).ToString());
                    }
                }
                else{
                    jsonResult.Append("No results were found. SOME ANOTHER MSG");
                }
            }
        }
    }
  return (ActionResult)new OkObjectResult($"{jsonResult}");
}