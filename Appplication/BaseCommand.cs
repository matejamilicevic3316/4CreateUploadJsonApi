namespace Appplication
{
    public interface BaseCommand<TReq, TResp>
    {
        TResp Execute(TReq req);
    }
}
