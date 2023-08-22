using backend.Models;

namespace backend.Interface
{
    public class IResponseData
    {
        public MenuModel? Menu { get; set; }
        public List<MenuModel>? MenuList { get; set; }
    }
    public class IResponse
    {
        public IResponse(int code,string mess, IResponseData? data)
        {
            this.StatusCode = code;
            this.Message = mess;
            if (data != null)
            {
                this.Data = data;
            }else { this.Data = null; }

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IResponseData? Data { get; set; }
    }
}
