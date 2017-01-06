using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NPOI;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.Util;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;


namespace gbsExtranetMVC.Helpers
{
    public static class CreateExcelFile_NPOI
    {
        public static void AddHeaderColumns(ref Dictionary<string, int> SheetColumns, ref int ColumnNumber, ref ISheet sheet, ref IRow headerRow)
        {

            foreach (var item in SheetColumns)
            {
                //(Optional) set the width of the columns
                sheet.SetColumnWidth(ColumnNumber, item.Value * 256);//11.29 = -0.79

                headerRow.CreateCell(ColumnNumber).SetCellValue(item.Key);

                ColumnNumber++;
            }

        }

        public static void FixHeader(ref ISheet sheet)
        {
            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);
        }

        public static ICellStyle CellStyle(ref XSSFWorkbook workbook, bool BoldFont, bool WrapText, bool? IsNumeric, HorizontalAlignment? HorizontalAlignment, VerticalAlignment? VerticalAlignment, string ValueFormat, short? ForegroundColor, FillPattern? FillPattern, short? RotateDegree)
        {
            var _CellStyle = workbook.CreateCellStyle();


            if (IsNumeric.HasValue)
            {
                if (IsNumeric.Value)
                {
                    _CellStyle.DataFormat = (short)CellType.Numeric;
                    _CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                }
            }

            if (BoldFont)
            {
                //Bold Font
                IFont _BoldFont = workbook.CreateFont();
                _BoldFont.Boldweight = (short)FontBoldWeight.Bold;
                _CellStyle.SetFont(_BoldFont);
            }

            //wrap
            if (WrapText) _CellStyle.WrapText = true;


            if (HorizontalAlignment.HasValue)
                //Centre Align
                _CellStyle.Alignment = HorizontalAlignment.Value;// XSSFCellStyle.ALIGN_CENTER_SELECTION;

            if (VerticalAlignment.HasValue)
                //Vertical Centre Align
                _CellStyle.VerticalAlignment = VerticalAlignment.Value;// XSSFCellStyle.VERTICAL_CENTER;


            if (ValueFormat != null)
                _CellStyle.DataFormat = (short)NPOI.SS.UserModel.BuiltinFormats.GetBuiltinFormat(ValueFormat);
            _CellStyle.DataFormat = (short)CellType.String;

            if (ForegroundColor.HasValue)
                _CellStyle.FillForegroundColor = ForegroundColor.Value; //  XSSFColor.SKY_BLUE.index;

            if (FillPattern.HasValue)
                _CellStyle.FillPattern = FillPattern.Value; // XSSFCellStyle.SOLID_FOREGROUND;

            if (RotateDegree.HasValue)
                _CellStyle.Rotation = RotateDegree.Value;

            return _CellStyle;

        }

        /// <summary>
        /// Last Update: 31/03/2015 By Usman Akram
        /// This Function will Convert the Format to Currency with specified Decimal Places and Prefix £ sign
        /// </summary>
        /// <param name="workbook">Reference to Current WorkBook</param>
        /// <param name="DecimalPlaces">Decimal Places Required for this Currency Format</param>
        /// <returns>XSSFCellStyle</returns>
        public static ICellStyle SetCellStyle_Currency(ref XSSFWorkbook workbook, string CurrencySymbol, int DecimalPlaces)
        {
            var DateTime_df = workbook.CreateDataFormat();

            string _decimalPlaced = "";
            for (int i = 1; i <= DecimalPlaces; i++)
            {
                if (i == 1)
                { _decimalPlaced += ".0"; }
                else
                { _decimalPlaced += "0"; }
            }

            short DateTime_dataFormat = DateTime_df.GetFormat(CurrencySymbol + "#,##0" + _decimalPlaced);

            var DateTime_style = workbook.CreateCellStyle();
            DateTime_style.DataFormat = DateTime_dataFormat;

            return DateTime_style;
        }



        /// <summary>
        /// Last Update: 31/03/2015 By Usman Akram
        /// This Function will Convert the Format to Date & Time with Time Specified Format 12 hour or 24 hour
        /// </summary>
        /// <param name="workbook">Reference to Current WorkBook</param>
        /// <param name="TimeFormat24Hour">True = 24 Hour Format e.g. 14:30 & False = 12 Hour Format e.g. 02:30 PM</param>
        /// <returns></returns>
        public static ICellStyle SetCellStyle_DateTime(ref XSSFWorkbook workbook, bool TimeFormat24Hour)
        {
            var DateTime_df = workbook.CreateDataFormat();

            string _timeFormat = " hh:mm tt";
            if (TimeFormat24Hour)
                _timeFormat = " HH:mm";

            short DateTime_dataFormat = DateTime_df.GetFormat("dd/MM/yyy" + _timeFormat);

            var DateTime_style = workbook.CreateCellStyle();
            DateTime_style.DataFormat = DateTime_dataFormat;

            return DateTime_style;
        }

        public static ICellStyle SetCellStyle_DateOnly(ref XSSFWorkbook workbook)
        {
            var DateTime_df = workbook.CreateDataFormat();
            short DateTime_dataFormat = DateTime_df.GetFormat("dd/MM/yyy");

            var DateTime_style = workbook.CreateCellStyle();
            DateTime_style.DataFormat = DateTime_dataFormat;

            return DateTime_style;
        }
    }
}