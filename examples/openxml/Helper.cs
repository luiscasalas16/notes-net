using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;

namespace example_net_excel
{
    internal class Helper
    {
        /// <summary>
        /// Open document.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static SpreadsheetDocument OpenDocument(MemoryStream s)
        {
            return SpreadsheetDocument.Open(s, false);
        }

        /// <summary>
        /// Get sheet name as DataTable from document.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <param name="firstRowIsHeader"></param>
        /// <returns></returns>
        public static DataTable GetSheet(SpreadsheetDocument doc, string name, bool firstRowIsHeader = true)
        {
            Sheet sheet = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().First(s => s.Name == name);

            Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

            int counter = 0;

            DataTable dt = new DataTable();

            foreach (Row row in rows)
            {
                counter += 1;

                if (counter == 1)
                {
                    //header
                    var j = 0;

                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        var colunmName = firstRowIsHeader ? GetSheetCell(doc, cell) : Convert.ToString(j++);

                        dt.Columns.Add(colunmName);
                    }
                }
                else
                {
                    //body
                    dt.Rows.Add();

                    int i = 0;

                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = GetSheetCell(doc, cell);

                        i++;
                    }
                }
            }

            return dt;
        }

        private static string GetSheetCell(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }

        /// <summary>
        /// Create empty document.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static SpreadsheetDocument CreateDocument(MemoryStream s)
        {
            var spreadSheet = SpreadsheetDocument.Create(s, SpreadsheetDocumentType.Workbook);

            var workbookPart = spreadSheet.AddWorkbookPart();

            workbookPart.Workbook = new Workbook();
            workbookPart.Workbook.Sheets = new Sheets();

            return spreadSheet;
        }

        /// <summary>
        /// Get document bytes;
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] GetDocumentBytes(MemoryStream s)
        {
            return s.ToArray();
        }

        /// <summary>
        /// Insert DataTable as sheet into document.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="dt"></param>
        public static void InsertSheet(SpreadsheetDocument doc, DataTable dt)
        {
            var sheetData = new SheetData();

            var sheetPart = doc.WorkbookPart.AddNewPart<WorksheetPart>();

            sheetPart.Worksheet = new Worksheet(sheetData);

            Sheets sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>();

            sheets.Append(new Sheet()
            {
                Id = doc.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = sheets.Elements<Sheet>().Count() > 0
                    ? sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1
                    : 1,
                Name = dt.TableName
            });

            //header

            Row headerRow = new Row();

            for (int n = 0; n < dt.Columns.Count; n++)
            {
                headerRow.Append(new Cell()
                {
                    CellValue = new CellValue(dt.Columns[n].ColumnName),
                    DataType = CellValues.String
                });
            }

            sheetData.Append(headerRow);

            //body

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row bodyRow = new Row();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    bodyRow.Append(InsetSheetCell(dt.Rows[i][j], dt.Columns[j].DataType));
                }

                sheetData.Append(bodyRow);
            }
        }

        private static Cell InsetSheetCell(object value, Type type)
        {
            if (type == typeof(DateTime))
            {
                Cell cell = new Cell()
                {
                    DataType = new EnumValue<CellValues>(CellValues.String),
                };

                if (value != DBNull.Value)
                {
                    cell.Append(new CellValue(((DateTime)value).ToString("MM/dd/yyyy h:mm tt")));
                }

                return cell;
            }
            if (type == typeof(long) || type == typeof(int) || type == typeof(short))
            {
                return new Cell() { CellValue = new CellValue(value.ToString()), DataType = CellValues.Number };
            }
            if (type == typeof(decimal) || type == typeof(float) || type == typeof(double))
            {
                return new Cell() { CellValue = new CellValue(value.ToString()), DataType = CellValues.Number };
            }
            else
            {
                return new Cell() { CellValue = new CellValue(value.ToString()), DataType = CellValues.String };
            }
        }
    }
}
