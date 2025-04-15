using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using System.Activities;
using System.ComponentModel;
using System.Diagnostics;

namespace RSalamow.Pdf
{
    public class ExtractPdfAttachments : CodeActivity // This base class exposes an OutArgument named Result
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> FilePath { get; set; }

        [Category("Output")]
        public OutArgument<List<string>> Files { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            List<string> files = new List<string>();
            string filePath = FilePath.Get(context);
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
                    if (File.Exists(fullPath))
                    {
                        // If the file already exists, create a new file name
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
                        string extension = Path.GetExtension(fullPath);
                        int counter = 1;
                        while (File.Exists(fullPath))
                        {
                            fullPath = Path.Combine(fileFolderPath, $"{fileNameWithoutExtension}_{counter}{extension}");
                            counter++;
                        }
                    }

                    //Extracts the attachment and saves it to the disk
                    FileStream stream = new FileStream(fullPath, FileMode.Create);
                    stream.Write(attachment.Data, 0, attachment.Data.Length);
                    files.Add(fullPath);
                    stream.Dispose();
                }
            }
            //Close the document
            loadedDocument.Close(true);
            Files.Set(context, files);
        }
    }
}
