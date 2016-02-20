using System;
using System.Linq;
using Artem.Google.Net;
using Artem.Google.UI;

/// <summary>
/// Utility helpers for GIS Functionality
/// Author: Jason Burton, Limitless Healthcare IT
/// Last Commit: 5/21/13
/// </summary>
namespace MDM
{
    public class GIS
    {
        public GIS()
        {
            //
            // Instantiate the GIS Class here..
            //
        }

        /// <summary>
        /// Returns Latitude and Longitude given any address.
        /// Author: Jason Burton Limitless Healthcare IT
        /// Using Google Geocoding API
        /// see:http://googlemap.codeplex.com/wikipage?title=Google%20Geocoder
        /// </summary>
        /// <param name="fullAddressToCheck"></param>
        /// <returns></returns>
        public string GetLatLong(String fullAddressToCheck)
        {

            try
            {
                string addressToCheck = fullAddressToCheck;

                // Make request to Google API
                GeoRequest gr = new GeoRequest(addressToCheck);
                GeoResponse gRes = gr.GetResponse();

                // Set Latitude/Longitude in textbox
               // txtCoordLatLong.Text = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
                return gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString(); //returns latitude,longitude as string
            }
            catch (Exception ex)
            {
                return string.Empty;
            }


           
        }


        /// <summary>
        /// Returns Latitude and Longitude given any address.
        /// Author: Jason Burton Limitless Healthcare IT
        /// Using Google Geocoding API
        /// see:http://googlemap.codeplex.com/wikipage?title=Google%20Geocoder
        /// </summary>
        /// <param name="fullAddressToCheck"></param>
        /// <returns></returns>
        public string GetAccuracy(String fullAddressToCheck)
        {

            try
            {
                string addressToCheck = fullAddressToCheck;

                // Make request to Google API
                GeoRequest gr = new GeoRequest(addressToCheck);
                GeoResponse gRes = gr.GetResponse();

                // Set Latitude/Longitude in textbox
                // txtCoordLatLong.Text = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
                ////return gRes.Results[0].Geometry.LocationType.ToString();

                string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();
                switch (resultLocationMatch)
                {
                    // See: http://stackoverflow.com/questions/3015370/how-to-get-the-equivalent-of-the-accuracy-in-google-map-geocoder-v3
                    case "ROOFTOP":
                        return "Precise";
                    break;
                    case "RANGE_INTERPOLATED":
                    return "Approximate";
                    break;
                    case "GEOMETRIC_CENTER":
                    return "Approximate";
                    break;
                    case "APPROXIMATE":
                    return "Approximate";
                    break;

                    default:
                        return resultLocationMatch;
                        break;
                }


               // return (string)gRes.Results[0].AddressComponents;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }



        }
    }
}