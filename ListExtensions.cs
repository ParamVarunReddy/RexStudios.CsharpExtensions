using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddToFront<T>(this List<T> list, T item)
        {
            if (ListExtensions.IsNullOrEmpty(list))
                list.Insert(0, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list.Count > 0 ? false : true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool CompareList<T>(this List<T> list1, List<T> list2)
        {
            //if any of the list is null, return false

            if ((!list1.IsNullOrEmpty() && list2.IsNullOrEmpty()) || (!list2.IsNullOrEmpty() && list1.IsNullOrEmpty()))
                return false;

            //if both lists are null, return true, since its same
            else if (list1 == null && list2 == null)
                return true;
            //if count don't match between 2 lists, then return false
            if (list1.Count != list2.Count)
                return false;
            bool IsEqual = true;
            foreach (T item in list1)
            {
                T Object1 = item;
                T Object2 = list2.ElementAt(list1.IndexOf(item));
                Type type = typeof(T);
                //if any of the object inside list is null and other list has some value for the same object  then return false
                if ((Object1 == null && Object2 != null) || (Object2 == null && Object1 != null))
                {
                    IsEqual = false;
                    break;
                }

                foreach (System.Reflection.PropertyInfo property in type.GetProperties())
                {
                    if (property.Name != "ExtensionData")
                    {
                        string Object1Value = string.Empty;
                        string Object2Value = string.Empty;
                        if (type.GetProperty(property.Name).GetValue(Object1, null) != null)

                            Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                        if (type.GetProperty(property.Name).GetValue(Object2, null) != null)
                            Object2Value = type.GetProperty(property.Name).GetValue(Object2, null).ToString();
                        //if any of the property value inside an object in the list didnt match, return false
                        if (Object1Value.Trim() != Object2Value.Trim())
                        {
                            IsEqual = false;
                            break;
                        }
                    }

                }

            }
            //if all the properties are same then return true
            return IsEqual;
        }

        /// <summary>
        /// https://dzone.com/articles/generic-extension-method-to-map-objects-from-one-t
        /// Generic Extension Method to Map Objects From One Type to Another
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination)
        where TSource : class, new()
        where TDestination : class, new()
        {
            if (source != null && destination != null)
            {
                List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
                List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                    if (destinationProperty != null)
                    {
                        try
                        {
                            destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

        }

        /// <summary>
        /// https://dzone.com/articles/generic-extension-method-to-map-objects-from-one-t
        /// Generic Extension Method to Map Objects From One Type to Another
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapProperties<TDestination>(this object source)
        where TDestination : class, new()
        {
            var destination = Activator.CreateInstance<TDestination>();
            MatchAndMap(source, destination);

            return destination;
        }

        /// <summary>
        /// https://www.extensionmethod.net/csharp/enum/generic-enum-to-list-t-converter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        /// <summary>
        /// Split list in to chunks of lists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        public static List<List<T>> SplitList<T>(List<T> items, int nSize)
        {
            List<List<T>> list = new List<List<T>>();
            for (int i = 0; i < items.Count; i += nSize)
                list.Add(items.GetRange(i, Math.Min(nSize, items.Count - i)));
            return list;
        }

        /// <summary>
        /// Extension method to write list data to excel.
        /// </summary>
        /// <typeparam name="T">Ganeric list</typeparam>
        /// <param name="list"></param>
        /// <param name="PathToSave">Path to save file.</param>
        //public static void ToExcel<T>(this List<T> list, string PathToSave)
        //{
        //    Excel Excel = new Workbook()
        //    #region Declarations

        //    if (string.IsNullOrEmpty(PathToSave))
        //    {
        //        throw new Exception("Invalid file path.");
        //    }
        //    else if (PathToSave.ToLower().Contains("") == false)
        //    {
        //        throw new Exception("Invalid file path.");
        //    }

        //    if (list == null)
        //    {
        //        throw new Exception("No data to export.");
        //    }

        //    Excel.Application excelApp = null;
        //    Excel.Workbooks books = null;
        //    Excel._Workbook book = null;
        //    Excel.Sheets sheets = null;
        //    Excel._Worksheet sheet = null;
        //    Excel.Range range = null;
        //    Excel.Font font = null;
        //    // Optional argument variable
        //    object optionalValue = Missing.Value;

        //    string strHeaderStart = "A2";
        //    string strDataStart = "A3";
        //    #endregion

        //    #region Processing


        //    try
        //    {
        //        #region Init Excel app.


        //        excelApp = new Excel.Application();
        //        books = (Excel.Workbooks)excelApp.Workbooks;
        //        book = (Excel._Workbook)(books.Add(optionalValue));
        //        sheets = (Excel.Sheets)book.Worksheets;
        //        sheet = (Excel._Worksheet)(sheets.get_Item(1));

        //        #endregion

        //        #region Creating Header


        //        Dictionary<string, string> objHeaders = new Dictionary<string, string>();

        //        PropertyInfo[] headerInfo = typeof(T).GetProperties();


        //        foreach (var property in headerInfo)
        //        {
        //            var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
        //                                    .Cast<DisplayNameAttribute>().FirstOrDefault();
        //            objHeaders.Add(property.Name, attribute == null ?
        //                                property.Name : attribute.DisplayName);
        //        }


        //        range = sheet.get_Range(strHeaderStart, optionalValue);
        //        range = range.get_Resize(1, objHeaders.Count);

        //        range.set_Value(optionalValue, objHeaders.Values.ToArray());
        //        range.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

        //        font = range.Font;
        //        font.Bold = true;
        //        range.Interior.Color = Color.LightGray.ToArgb();

        //        #endregion

        //        #region Writing data to cell


        //        int count = list.Count;
        //        object[,] objData = new object[count, objHeaders.Count];

        //        for (int j = 0; j < count; j++)
        //        {
        //            var item = list[j];
        //            int i = 0;
        //            foreach (KeyValuePair<string, string> entry in objHeaders)
        //            {
        //                var y = typeof(T).InvokeMember(entry.Key.ToString(), BindingFlags.GetProperty, null, item, null);
        //                objData[j, i++] = (y == null) ? "" : y.ToString();
        //            }
        //        }


        //        range = sheet.get_Range(strDataStart, optionalValue);
        //        range = range.get_Resize(count, objHeaders.Count);

        //        range.set_Value(optionalValue, objData);
        //        range.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

        //        range = sheet.get_Range(strHeaderStart, optionalValue);
        //        range = range.get_Resize(count + 1, objHeaders.Count);
        //        range.Columns.AutoFit();

        //        #endregion

        //        #region Saving data and Opening Excel file.


        //        if (string.IsNullOrEmpty(PathToSave) == false)
        //            book.SaveAs(PathToSave);

        //        excelApp.Visible = true;

        //        #endregion

        //        #region Release objects

        //        try
        //        {
        //            if (sheet != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
        //            sheet = null;

        //            if (sheets != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
        //            sheets = null;

        //            if (book != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
        //            book = null;

        //            if (books != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(books);
        //            books = null;

        //            if (excelApp != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        //            excelApp = null;
        //        }
        //        catch (Exception ex)
        //        {
        //            sheet = null;
        //            sheets = null;
        //            book = null;
        //            books = null;
        //            excelApp = null;
        //        }
        //        finally
        //        {
        //            GC.Collect();
        //        }

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    #endregion
        //}

        //


    }
}
