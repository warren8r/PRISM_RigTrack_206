using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Modules_RigTrack_NFVTestPage : System.Web.UI.Page
{
   


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn1_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.SurveyDTO parsedSurvey = new RigTrack.DatabaseTransferObjects.SurveyDTO();
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> surveyList = new List<RigTrack.DatabaseTransferObjects.SurveyDTO>();
        if (FileUpload1.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);

            List<string> returnedData =  ParseContent(FileUpload1.PostedFile.InputStream);
            foreach (string s in returnedData)
            {
                
                string[] values = s.Split(new [] {" "}, StringSplitOptions.RemoveEmptyEntries);
                parsedSurvey.MD = double.Parse(values[0].ToString());
                parsedSurvey.INC = double.Parse(values[1].ToString());
                parsedSurvey.Azimuth = double.Parse(values[2].ToString());
                parsedSurvey.TVD = double.Parse(values[3].ToString());
                parsedSurvey.NS = double.Parse(values[4].ToString());
                parsedSurvey.EW = double.Parse(values[5].ToString());
                surveyList.Add(parsedSurvey);
            }
        }
        surveyList.Count();
    }

    private List<string> ParseContent(System.IO.Stream stream)
    {
        string survey;
        bool foundSurveys = false;
        bool header = false;
        List<string> surveyList = new List<string>();
        using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.ASCII))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (foundSurveys)
                {
                    if (!header)
                    {
                        header = true;
                    }
                    else
                    {
                        survey = line;
                        survey.Trim('\t');
                        surveyList.Add(survey);
                    }
                }
                if (line == "H SURVEY LIST")
                {
                    foundSurveys = true;
                }
            }
        }
        return surveyList;
    }
}