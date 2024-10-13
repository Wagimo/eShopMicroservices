using Newtonsoft.Json.Linq;

namespace BuildingBlocks.UtilsMethods
{
    public static class HttpRequestRawData
    {
        public static async Task<JObject> RawData ( Stream streamBody )
        {
            string rawContent = string.Empty;
            using (StreamReader stream = new ( streamBody ))
            {
                rawContent = await stream.ReadToEndAsync ();
            }

            return JObject.Parse ( rawContent );
        }
    }
}
