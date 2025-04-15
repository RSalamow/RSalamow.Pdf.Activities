using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

List<string> files = new List<string>();
string filePath = @"C:\Users\rsala\Downloads\Result.pdf";
string fileFolderPath = Path.GetDirectoryName(filePath);
// Get current working directory
string currentDirectory = Directory.GetCurrentDirectory();
//Load the PDF document
byte[] pdfBytes = File.ReadAllBytes(filePath);
PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdfBytes);
//Get first page from document
PdfLoadedPage page = loadedDocument.Pages[0] as PdfLoadedPage;
//Get the annotation collection from pages
PdfLoadedAnnotationCollection annotations = page.Annotations;
//Iterates the annotations
foreach (PdfLoadedAnnotation annot in annotations)
{
    //Check for the attachment annotation
    if (annot is PdfLoadedAttachmentAnnotation)
    {
        PdfLoadedAttachmentAnnotation file = annot as PdfLoadedAttachmentAnnotation;
        //Extracts the attachment and saves it to the disk
        FileStream stream = new FileStream(file.FileName, FileMode.Create);
        stream.Write(file.Data, 0, file.Data.Length);
        stream.Dispose();
    }
}
//Iterates through the attachments
if (loadedDocument.Attachments != null && loadedDocument.Attachments.Count != 0)
{
    foreach (PdfAttachment attachment in loadedDocument.Attachments)
    {
        string fullPath = fileFolderPath + "\\" + attachment.FileName;
        //Extracts the attachment and saves it to the disk
        FileStream stream = new FileStream(fullPath, FileMode.Create);
        stream.Write(attachment.Data, 0, attachment.Data.Length);
        files.Add(fullPath);
        stream.Dispose();
    }
}
