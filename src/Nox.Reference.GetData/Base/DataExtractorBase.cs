//namespace Nox.Reference.GetData.Base
//{
//    internal abstract class DataExtractorBase<T>
//    {
//        public async Task ExtractData()
//        {
//            try
//            { 
//                var data = await PrepareAndGetData();
//                data = await TransformData(data);
//                data = await FixData(data);
//                SaveData(data);
//            }
//            catch (Exception ex)
//            {
//                Console.Write(ex.Message);
//            }
//        }

//        public abstract Task<T> FixData(T data);

//        public abstract Task<T> PrepareAndGetData();

//        public abstract Task<T> TransformData(T data);

//        public abstract string SaveData(T data);
//    }
//}
