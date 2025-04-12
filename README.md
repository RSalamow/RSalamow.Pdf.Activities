# ExtractPdfAttachment - Custom UiPath Activity

**ExtractPdfAttachment** is a custom activity for UiPath that extracts embedded file attachments from a single PDF document and saves them in the same folder as the PDF.  
Built using **.NET 6 (C#)**, it is compatible with modern UiPath Studio versions.

---

## Features
- Extracts **embedded attachments** from a PDF file.
- Saves extracted attachments automatically next to the original PDF.
- Returns a list of saved file paths.
- Lightweight, fast, and easy to use in UiPath workflows.

---

## Installation

1. Build the project in Visual Studio (`Release` mode).
2. Locate the `.nupkg` file in the `bin/Release` folder.
3. In UiPath Studio:
   - Go to **Manage Packages** → **Settings**.
   - Add a new package source pointing to your local `.nupkg` file or NuGet feed.
   - Install **ExtractPdfAttachment** into your workflow.

---

## How to Use

1. Drag and drop the **ExtractPdfAttachment** activity into your UiPath workflow.
2. Set the required property:

| Property | Description | Type |
| :--- | :--- | :--- |
| `FilePath` | Full file path of the input PDF. | `String` |

3. The activity will output:

| Property | Description | Type |
| :--- | :--- | :--- |
| `ExtractedFiles` | List of full file paths of the extracted attachments. | `List<String>` |

---

## Properties

| Name | Direction | Description |
| :--- | :--- | :--- |
| `FilePath` | In | Path of the PDF file to process. |
| `ExtractedFiles` | Out | List of file paths of the saved attachments. |

---

## Example

```plaintext
Input:
    FilePath: "C:\Documents\report.pdf"

After running the activity:
    Attachments will be saved in "C:\Documents\"
    
Output:
    ExtractedFiles: [
        "C:\Documents\invoice.xlsx",
        "C:\Documents\contract.docx"
    ]
```

Example usage in UiPath:

1. Provide the path to the PDF file.
2. Run the activity.
3. Process the extracted files as needed (move, read, rename, etc.).

---

## Requirements

- UiPath Studio 2022.4 or later
- .NET 6 Runtime
- PDF must contain embedded attachments

---

## Development

This activity is built using:
- **C# (.NET 6)**
- **UiPath.Activities.SDK**

It is designed for modern cross-platform UiPath automation projects.

---

## License

MIT License.

---

## Support

For issues, questions, or feature requests, please create an issue or contact the maintainer.
