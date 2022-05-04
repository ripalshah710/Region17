using System;


/// <summary>
/// Summary description for Attendee
/// </summary>

namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class Attendee : region4.ObjectModel.Attendee
    {

        public Attendee(Session session, User user)
            : base(session, user)
        {
        }

        public Attendee(int attendee_pk)
            : base(attendee_pk)
        {
        }

    }

    [Serializable]
    public class SessionRegistration : region4.ObjectModel.SessionRegistration
    {
        public SessionRegistration(Session session, User user)
            : base(session, user)
        {
        }

        public SessionRegistration(Session session, User user, bool overrideLimit)
        :base(session,user,overrideLimit)
        {        
        }
    }
}