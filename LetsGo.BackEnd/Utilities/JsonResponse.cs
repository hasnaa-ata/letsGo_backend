using System.Collections.Generic;

namespace Classes.Utilities
{
    public class JsonResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public string RedirectTo { get; set; }
        public IEnumerable<string> Errors { get; set; }
        //public ResponseCallBackModal CallBackModal { get; set; }
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }

    public class MobileJsonResponse<T>
    {
        public IEnumerable<T> data { get; set; }
        public string message { get; set; }
        public int success { get; set; }
    }

    //public class ResponseCallBackModal
    //{
    //    public int ReferenceId { get; set; }
    //    public string ReferenceLink { get; set; }
    //    public bool IsPopupWindow { get; set; }
    //    public string PopupWindowClass { get; set; }
    //    public string NgControllerName { get; set; }
    //}
}