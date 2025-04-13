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
        /*
         * The returned value will be used to set the value of the Result argument
         */
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> FilePath { get; set; }

        [Category("Output")]
        public OutArgument<List<string>> Files { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var files = new List<string>();
            var filePath = FilePath.Get(context);
            var fileFolderPath = Path.GetDirectoryName(filePath);

            // Load the PDF document
            var pdfBytes = File.ReadAllBytes(filePath);
            using var loadedDocument = new PdfLoadedDocument(pdfBytes);

            // Extract annotations
            ExtractAnnotations(loadedDocument, files);

            // Extract attachments
            ExtractAttachments(loadedDocument, fileFolderPath, files);

            Files.Set(context, files);
        }

        private void ExtractAnnotations(PdfLoadedDocument loadedDocument, List<string> files)
        {
            var page = loadedDocument.Pages[0] as PdfLoadedPage;
            var annotations = page.Annotations;

            foreach (PdfLoadedAnnotation annot in annotations)
            {
                if (annot is PdfLoadedAttachmentAnnotation attachmentAnnotation)
                {
                    SaveAttachmentToFile(attachmentAnnotation.FileName, attachmentAnnotation.Data);
                }
            }
        }

        private void ExtractAttachments(PdfLoadedDocument loadedDocument, string fileFolderPath, List<string> files)
        {
            foreach (PdfAttachment attachment in loadedDocument.Attachments)
            {
                var fullPath = Path.Combine(fileFolderPath, attachment.FileName);
                SaveAttachmentToFile(fullPath, attachment.Data);
                files.Add(fullPath);
            }
        }

        private void SaveAttachmentToFile(string filePath, byte[] data)
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            stream.Write(data, 0, data.Length);
        }

        public void ExecuteInternal()
        {
            // Use this to automatically attach the debugger to the process
            // Debugger.Launch();
            throw new NotImplementedException();
        }
    }
}
