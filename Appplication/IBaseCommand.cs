namespace Appplication
{
    public interface IBaseCommand<TReq, TResp>
    {
        TResp Execute(TReq req);
    }
}
