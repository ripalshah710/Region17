using System;
using System.Collections.Generic;
using System.Text;

namespace escWeb.tx_r17.ObjectModel
{
    [Serializable]
    public class TranscriptItem : region4.ObjectModel.TranscriptItem
    {
        public TranscriptItem(int item_pk)
            : base(item_pk)
        {

        }

        protected override void LoadCustomerSpecificData(System.Data.SqlClient.SqlCommand cmd)
        {
            
        }
    }
}
