using System;
using System.Linq;

namespace ObjectConfig
{
    public class ObjectAccessor
    {
        private object parentObject;
        private object backupObject;
        public ObjectAccessor(object parentObject)
        {
            this.parentObject = parentObject;
            this.backupObject = parentObject;
        }

        public object GetOriginalObject()
        {
            return backupObject;
        }

        public object GetUpdatedObject()
        {
            return parentObject;
        }

        public object PopulateObjectValueByString(string fieldToPopulate, object valueToPopulate)
        {
            if (fieldToPopulate.Contains("."))
            {
                string[] classFields = fieldToPopulate.Split('.');

                object value = parentObject;
                Type parentType = value.GetType();
                foreach (string classField in classFields)
                {

                    if (classFields.Last() != classField)
                    {
                        value = parentType.GetProperty(classField).GetValue(value, null);
                        parentType = value.GetType();
                    }
                    else
                    {
                        if (value != null)
                        {
                            Type type = value.GetType();
                            type.GetProperty(classField).SetValue(value, valueToPopulate);
                        }
                    }
                }
            }
            else
            {
                parentObject.GetType().GetProperty(fieldToPopulate).SetValue(parentObject, valueToPopulate);
            }
            return parentObject;
        }

        public object GetObjectValueByString(string fieldToGet)
        {
            object value = parentObject;
            if (fieldToGet.Contains("."))
            {
                string[] classFields = fieldToGet.Split('.');

                //object value = parentObject;
                Type parentType = value.GetType();
                foreach (string classField in classFields)
                {

                    if (classFields.Last() != classField)
                    {
                        value = parentType.GetProperty(classField).GetValue(value, null);
                        parentType = value.GetType();

                    }
                    else
                    {
                        if (value != null)
                        {
                            Type type = value.GetType();
                            value = type.GetProperty(classField).GetValue(value);
                        }
                    }
                }
            }
            else
            {
                value = parentObject.GetType().GetProperty(fieldToGet).GetValue(parentObject);
            }
            return value;
        }
    }
}
