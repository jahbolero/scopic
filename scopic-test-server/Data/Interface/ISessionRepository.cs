using System;

namespace scopic_test_server.Interface
{
    public interface ISessionRepository
    {
        Guid AddSession();
        void DestorySession(Guid SessionId);
        bool ValidateSession(Guid SessionId);

    }
}