using NetPartyCore.Framework;

namespace NetPartyTest.Framework
{
    class ControllerMock: CoreController
    {
        public bool MethodMock()
        {
            return GetSerivce<IServiceMock>().MethodMock();
        }
    }
}
