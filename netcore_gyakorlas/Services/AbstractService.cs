 using netcore_gyakorlas.UnitOfWork;

 namespace EventApp.Services
{
    public class AbstractService
    {
        protected IUnitOfWork UnitOfWork;

        public AbstractService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
