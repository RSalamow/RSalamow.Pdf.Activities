// See https://aka.ms/new-console-template for more information
using RSalamow.Pdf;

Console.WriteLine("Hello, World!");

var extractor = new ExtractPdfAttachments();

extractor.FilePath = @"C:\Users\rsala\Documents\UiPath\Pdf-Attachment-Test\Data\example_041.pdf";

Console.WriteLine("extractor.Files.");