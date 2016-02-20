<%@ WebHandler Language="C#" Class="DownloadHandler" %>

using System;
using System.Web;

public class DownloadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {

        string fileName = context.Request.QueryString["fileName"];

        string filePath = context.Request.QueryString["filePath"];
        //string path = Server.MapPath("../../Documents/" + lbl_docname.Text);
        //byte[] bts = System.IO.File.ReadAllBytes(filePath);
        //context.Response.Clear();
        //context.Response.ClearHeaders();
        //context.Response.AddHeader("Content-Type", "Application/octet-stream");
        //context.Response.AddHeader("Content-Length", bts.Length.ToString());
        //context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        //context.Response.BinaryWrite(bts);
        //context.Response.Flush();
        //context.Response.End();
        context.Response.Clear();
        context.Response.ContentType = "Application/octet-stream";
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        string fileSeverPath = context.Server.MapPath(filePath);
        if (fileSeverPath != null)
        {
            byte[] fileBytes = GetFileBytes(fileSeverPath);
            context.Response.BinaryWrite(fileBytes);
            context.Response.Flush();
            context.Response.End();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    protected byte[] GetFileBytes(string url)
    {
        System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
        byte[] fileBytes = null;
        byte[] buffer = new byte[4096];
        System.Net.WebResponse webResponse = webRequest.GetResponse();
        try
        {
            System.IO.Stream stream = webResponse.GetResponseStream();
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            int chunkSize = 0;
            do
            {
                chunkSize = stream.Read(buffer, 0, buffer.Length);
                memoryStream.Write(buffer, 0, chunkSize);
            } while (chunkSize != 0);

            fileBytes = memoryStream.ToArray();

        }
        catch (Exception ex)
        {

            // log it somewhere

        }
        return fileBytes;

    }

}