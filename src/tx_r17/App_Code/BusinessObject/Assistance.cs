using System;
using System.Collections.Generic;
using System.Text;

using Csla;
using Csla.Security;
using System.Data.SqlClient;
using System.Data;
using Csla.Data;
using System.Configuration;

namespace tx_r17.BusinessObect.Accountability
{
    [Serializable()]
    public class Assistance : BusinessBase<Assistance>
    {
        #region Properties

        protected static PropertyInfo<int> ObjIdProperty = RegisterProperty<int>(new PropertyInfo<int>("obj_id"));
        public int ObjId
        {
            get { return GetProperty(ObjIdProperty); }
            set { SetProperty(ObjIdProperty, value); }
        }

        protected static PropertyInfo<Guid> SidProperty = RegisterProperty<Guid>(new PropertyInfo<Guid>("sid"));
        public Guid Sid
        {
            get { return GetProperty(SidProperty); }
            set { SetProperty(SidProperty, value); }
        }

        protected static PropertyInfo<int> User_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("user_id"));
        public int User_Id
        {
            get { return GetProperty(User_IdProperty); }
            set { SetProperty(User_IdProperty, value); }
        }

        protected static PropertyInfo<int> Room_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("room_id"));
        public int Room_Id
        {
            get { return GetProperty(Room_IdProperty); }
            set { SetProperty(Room_IdProperty, value); }
        }

        protected static PropertyInfo<string> Room_NameProperty = RegisterProperty<string>(new PropertyInfo<string>("specific_site"));
        public string Room_Name
        {
            get { return GetProperty(Room_NameProperty); }
        }

        protected static PropertyInfo<int> Site_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("site_id"));
        public int Site_Id
        {
            get { return GetProperty(Site_IdProperty); }
            set { SetProperty(Site_IdProperty, value); }
        }

        protected static PropertyInfo<string> Site_NameProperty = RegisterProperty<string>(new PropertyInfo<string>("client"));
        public string Site_Name
        {
            get { return GetProperty(Site_NameProperty); }
        }

        protected static PropertyInfo<int> SiteType_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("site_type_id"));
        public int SiteType_Id
        {
            get { return GetProperty(SiteType_IdProperty); }
            set { SetProperty(SiteType_IdProperty, value); }
        }

        protected static PropertyInfo<int> Org_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("org_id"));
        public int Org_Id
        {
            get { return GetProperty(Org_IdProperty); }
            set { SetProperty(Org_IdProperty, value); }
        }

        protected static PropertyInfo<int> Focus_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("focus_id"));
        public int Focus_Id
        {
            get { return GetProperty(Focus_IdProperty); }
            set { SetProperty(Focus_IdProperty, value); }
        }

        protected static PropertyInfo<string> Focus_NameProperty = RegisterProperty<string>(new PropertyInfo<string>("point"));
        public string Focus_Name
        {
            get { return GetProperty(Focus_NameProperty); }
        }

        protected static PropertyInfo<int> Team_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("team_id"));
        public int Team_Id
        {
            get { return GetProperty(Team_IdProperty); }
            set { SetProperty(Team_IdProperty, value); }
        }

        protected static PropertyInfo<int> Department_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("department_id"));
        public int Department_Id
        {
            get { return GetProperty(Department_IdProperty); }
            set { SetProperty(Department_IdProperty, value); }
        }

        protected static PropertyInfo<int> Division_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("division_id"));
        public int Division_Id
        {
            get { return GetProperty(Division_IdProperty); }
            set { SetProperty(Division_IdProperty, value); }
        }

        protected static PropertyInfo<int> Service_Hour_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("service_hour_id"));
        public int Service_Hour_Id
        {
            get { return GetProperty(Service_Hour_IdProperty); }
            set { SetProperty(Service_Hour_IdProperty, value); }
        }

        protected static PropertyInfo<double> Service_LengthProperty = RegisterProperty<double>(new PropertyInfo<double>("totHours"));
        public double Service_Length
        {
            get { return GetProperty(Service_LengthProperty); }
        }

        protected static PropertyInfo<int> Contact_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("contact_id"));
        public int Contact_Id
        {
            get { return GetProperty(Contact_IdProperty); }
            set { SetProperty(Contact_IdProperty, value); }
        }

        protected static PropertyInfo<int> Activity_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("activity_id"));
        public int Activity_Id
        {
            get { return GetProperty(Activity_IdProperty); }
            set { SetProperty(Activity_IdProperty, value); }
        }

        protected static PropertyInfo<string> Activity_NameProperty = RegisterProperty<string>(new PropertyInfo<string>("activity"));
        public string Activity_Name
        {
            get { return GetProperty(Activity_NameProperty); }
            set { SetProperty(Activity_NameProperty, value); }
        }

        protected static PropertyInfo<DateTime> Service_DateProperty = RegisterProperty<DateTime>(new PropertyInfo<DateTime>("date"));
        public DateTime Service_Date
        {
            get { return GetProperty(Service_DateProperty); }
            set
            {
                SetProperty(Service_DateProperty, value);

            }
        }

        protected static PropertyInfo<int> Budget_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("budget_id"));
        public int Budget_Id
        {
            get { return GetProperty(Budget_IdProperty); }
            set { SetProperty(Budget_IdProperty, value); }
        }

        protected static PropertyInfo<int> Travel_Hour_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("travel_hour_id"));
        public int Travel_Hour_Id
        {
            get { return GetProperty(Travel_Hour_IdProperty); }
            set { SetProperty(Travel_Hour_IdProperty, value); }
        }

        protected static PropertyInfo<bool> Cross_Divisional_FocusProperty = RegisterProperty<bool>(new PropertyInfo<bool>("cross_divisional_focus"));
        public bool Cross_Divisional_Focus
        {
            get { return GetProperty(Cross_Divisional_FocusProperty); }
            set { SetProperty(Cross_Divisional_FocusProperty, value); }
        }

        public static PropertyInfo<int> TEC_Purpose_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("tec_purpose_id"));
        public int TEC_Purpose_Id
        {
            get { return GetProperty(TEC_Purpose_IdProperty); }
            set { SetProperty(TEC_Purpose_IdProperty, value); }
        }

        protected static PropertyInfo<int> ESC_Strand_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("esc_strand_id"));
        public int ESC_Strand_Id
        {
            get { return GetProperty(ESC_Strand_IdProperty); }
            set { SetProperty(ESC_Strand_IdProperty, value); }
        }

        protected static PropertyInfo<int> ESC_U_Competency_IdProperty = RegisterProperty<int>(new PropertyInfo<int>("esc_u_competency_id"));
        public int ESC_U_Competency_Id
        {
            get { return GetProperty(ESC_U_Competency_IdProperty); }
            set { SetProperty(ESC_U_Competency_IdProperty, value); }
        }

        protected static PropertyInfo<Guid> Manager_SidProperty = RegisterProperty<Guid>(new PropertyInfo<Guid>("manager_sid"));
        public Guid Manager_Sid
        {
            get { return GetProperty(Manager_SidProperty); }
            set { SetProperty(Manager_SidProperty, value); }
        }

        protected static PropertyInfo<string> CommentsProperty = RegisterProperty<string>(new PropertyInfo<string>("comments"));
        public string Comments
        {
            get { return GetProperty(CommentsProperty); }
            set { SetProperty(CommentsProperty, value); }
        }

        protected static PropertyInfo<string> ECampusProperty = RegisterProperty<string>(new PropertyInfo<string>("ecampus"));
        public string ECampus
        {
            get { return GetProperty(ECampusProperty); }
            set { SetProperty(ECampusProperty, value); }
        }

        protected static PropertyInfo<double> Service_Hour_AmountProperty = RegisterProperty<double>(new PropertyInfo<double>("service_hour_amount"));
        public double Service_Hour_Amount
        {
            get { return GetProperty(Service_Hour_AmountProperty); }
            set { SetProperty(Service_Hour_AmountProperty, value); }
        }

        protected static PropertyInfo<double> Travel_Hour_AmountProperty = RegisterProperty<double>(new PropertyInfo<double>("travel_hour_amount"));
        public double Travel_Hour_Amount
        {
            get { return GetProperty(Travel_Hour_AmountProperty); }
            set { SetProperty(Travel_Hour_AmountProperty, value); }
        }

        //Added by VM 10-2-2018
        private static PropertyInfo<bool> Is22bMassCommunicationProperty = RegisterProperty<bool>(new PropertyInfo<bool>("is22bMassCommunication"));
        public bool Is22bMassCommunication
        {
            get { return GetProperty(Is22bMassCommunicationProperty); }
            set { SetProperty(Is22bMassCommunicationProperty, value); }
        }

        //Child Objects Properties


        //Added by VM 9-26-2012

        private static PropertyInfo<bool> IsSchoolFinanceRelatedProperty = RegisterProperty<bool>(new PropertyInfo<bool>("isSchoolFinanceRelated"));
        public bool IsSchoolFinanceRelated
        {
            get { return GetProperty(IsSchoolFinanceRelatedProperty); }
            set { SetProperty(IsSchoolFinanceRelatedProperty, value); }
        }

        private static PropertyInfo<bool> IsExtendedLearningOpportunitiesProperty = RegisterProperty<bool>(new PropertyInfo<bool>("ExtendedLearningOpportunities"));
        public bool IsExtendedLearningOpportunities
        {
            get { return GetProperty(IsExtendedLearningOpportunitiesProperty); }
            set { SetProperty(IsExtendedLearningOpportunitiesProperty, value); }
        }

        private static PropertyInfo<bool> IsCoreServiceProperty = RegisterProperty<bool>(new PropertyInfo<bool>("CoreService"));
        public bool IsCoreService
        {
            get { return GetProperty(IsCoreServiceProperty); }
            set { SetProperty(IsCoreServiceProperty, value); }
        }

        protected static PropertyInfo<int> FundingIdProperty = RegisterProperty<int>(new PropertyInfo<int>("funding_id"));
        public int FundingId
        {
            get { return GetProperty(FundingIdProperty); }
            set { SetProperty(FundingIdProperty, value); }
        }
        private static PropertyInfo<FinancialIntegritys> FinancialIntegritysProperty =
      RegisterProperty<FinancialIntegritys>(new PropertyInfo<FinancialIntegritys>("FinancialIntegritys"));
        public FinancialIntegritys FinancialIntegritys
        {
            get { return GetProperty(FinancialIntegritysProperty); }
            set { SetProperty(FinancialIntegritysProperty, value); }
        }

        private static PropertyInfo<IncreaseStudentPerformances> IncreaseStudentPerformancesProperty =
        RegisterProperty<IncreaseStudentPerformances>(new PropertyInfo<IncreaseStudentPerformances>("IncreaseStudentPerformances"));
        public IncreaseStudentPerformances IncreaseStudentPerformances
        {
            get { return GetProperty(IncreaseStudentPerformancesProperty); }
            set { SetProperty(IncreaseStudentPerformancesProperty, value); }
        }

        private static PropertyInfo<PostSecondaryCredits> PostSecondaryCreditsProperty =
        RegisterProperty<PostSecondaryCredits>(new PropertyInfo<PostSecondaryCredits>("PostSecondaryCredits"));
        public PostSecondaryCredits PostSecondaryCredits
        {
            get { return GetProperty(PostSecondaryCreditsProperty); }
            set { SetProperty(PostSecondaryCreditsProperty, value); }
        }

        private static PropertyInfo<NonFirstStandardFinancialAssistances> NonFirstStandardFinancialAssistancesProperty =
        RegisterProperty<NonFirstStandardFinancialAssistances>(new PropertyInfo<NonFirstStandardFinancialAssistances>("NonFirstStandardFinancialAssistances"));
        public NonFirstStandardFinancialAssistances NonFirstStandardFinancialAssistances
        {
            get { return GetProperty(NonFirstStandardFinancialAssistancesProperty); }
            set { SetProperty(NonFirstStandardFinancialAssistancesProperty, value); }
        }

        protected static PropertyInfo<LocationAudiences> LocationAudiencesProperty =
           RegisterProperty<LocationAudiences>(new PropertyInfo<LocationAudiences>("LocationAudiences"));
        public LocationAudiences LocationAudiences
        {
            get { return GetProperty(LocationAudiencesProperty); }
            set { SetProperty(LocationAudiencesProperty, value); }
        }

        //Added by VM 6-19-2018
        protected static PropertyInfo<StrategicPrioritys> StrategicPrioritysProperty =
        RegisterProperty<StrategicPrioritys>(new PropertyInfo<StrategicPrioritys>("StrategicPrioritys"));
        public StrategicPrioritys StrategicPrioritys
        {
            get { return GetProperty(StrategicPrioritysProperty); }
            set { SetProperty(StrategicPrioritysProperty, value); }
        }

        #endregion

        #region Rules

        protected override void AddBusinessRules()
        {

            ValidationRules.AddRule(ServiceDateRule, Service_DateProperty);
            ValidationRules.AddRule(ServiceLength, Service_Hour_IdProperty);
            ValidationRules.AddRule(ContactMethodRule, Contact_IdProperty);
            ValidationRules.AddRule(ActivityRule, Activity_IdProperty);
            ValidationRules.AddRule(BudgetCodeRule, Activity_IdProperty);
            ValidationRules.AddRule(OrganizationsRule, LocationAudiencesProperty);
            ValidationRules.AddRule(FundingRule, FundingIdProperty);
            ValidationRules.AddRule(ProjectRule, TEC_Purpose_IdProperty);
            ValidationRules.AddRule(ContractRule, Focus_IdProperty);

        }

        private static bool ServiceDateRule(object target, Csla.Validation.RuleArgs e)
        {
            if (((Assistance)target).Service_Date == DateTime.MinValue)
            {
                e.Description = "The service date field is required.";
                return false;
            }
            else
                return true;
        }

        private static bool ServiceLength(object target, Csla.Validation.RuleArgs e)
        {

            if (((Assistance)target).Service_Hour_Id == 0 && ((Assistance)target).Travel_Hour_Id == 0)
            {
                e.Description = "The service length field is required.";
                return false;
            }
            else
                return true;
        }

        private static bool ContactMethodRule(object target, Csla.Validation.RuleArgs e)
        {
            if (((Assistance)target).Contact_Id == 0 && !((Assistance)target).Activity_Name.ToLower().Contains("leave") && ((Assistance)target).Activity_Name.ToLower() != "jury duty" && ((Assistance)target).Travel_Hour_Id == 0 && (ConfigurationManager.AppSettings["accountability.ta"].Contains(((Assistance)target).Activity_Id.ToString())))
            {
                e.Description = "The contact method field is required.";
                return false;
            }
            else
                return true;
        }

        private static bool ActivityRule(object target, Csla.Validation.RuleArgs e)
        {
            if (((Assistance)target).Activity_Id == 0 && ((Assistance)target).Travel_Hour_Id == 0)
            {
                e.Description = "The activity field is required.";
                return false;
            }
            else
                return true;
        }

        private static bool BudgetCodeRule(object target, Csla.Validation.RuleArgs e)
        {
            BudgetInfo bi = BudgetInfo.GetBudgetInfoByObjId(((Assistance)target).Budget_Id);
            if (((Assistance)target).Budget_Id == 0 && ((Assistance)target).Activity_Name.ToLower() != "technical assistance")
            {
                e.Description = "The budget code  is required.";
                return false;
            }

            if (!bi.Active && ((Assistance)target).Activity_Name.ToLower() != "technical assistance")
            {
                e.Description = "The budget code  is invalid.";
                return false;
            }
            else
                return true;
        }

        private static bool OrganizationsRule(object target, Csla.Validation.RuleArgs e)
        {
            if (((Assistance)target).LocationAudiences.Count == 0 && !((Assistance)target).Activity_Name.ToLower().Contains("leave") && ((Assistance)target).Activity_Name.ToLower() != "jury duty" && ((Assistance)target).Travel_Hour_Id == 0 && (ConfigurationManager.AppSettings["accountability.ta"].Contains(((Assistance)target).Activity_Id.ToString())))
            {
                e.Description = "The Location/Audience/#Served field is required.";
                return false;
            }
            else
                return true;
        }

        private static bool FundingRule(object target, Csla.Validation.RuleArgs e)
        {

            if (((Assistance)target).FundingId == 0)
            {
                e.Description = "The Funding field is required.";
                return false;
            }
            else
                return true;
        }

        private static bool ProjectRule(object target, Csla.Validation.RuleArgs e)
        {

            if (((Assistance)target).TEC_Purpose_Id == 0)
            {
                e.Description = "The Project field is required.";
                return false;
            }
            else
                return true;
        }


        
        private static bool ContractRule(object target, Csla.Validation.RuleArgs e)
       
        {

            if (((Assistance)target).Focus_Id == 0)
            {
                e.Description = "The Contract field is required.";
                return false;
            }
            else
                return true;
        }


        /*private static bool PointRule(object target, Csla.Validation.RuleArgs e)
        {
            if (((Assistance)target).Travel_Hour_Id > 0)
                return true;

            if (((Assistance)target).Focus_Id == 0 && !((Assistance)target).Activity_Name.ToLower().Contains("leave") && ((Assistance)target).Activity_Name.ToLower() != "jury duty")
            {
                e.Description = "The point field is required.";
                return false;
            }
            else
                return true;
        }
        */
        /*private static bool FundingRule(object target, Csla.Validation.RuleArgs e)
        {
            if (((Assistance)target).FundingId == 0)
            {
                e.Description = "The funding field is required.";
                return false;
            }
            else
                return true;
        }*/
        #endregion

        #region Business Methods

        public Assistance CallCheckRules(Assistance a)
        {
            ValidationRules.CheckRules();
            return a;
        }

        public bool isEditable()
        {
            ConfigurationInfoList list = ConfigurationInfoList.GetConfigurationInfoList(Service_Date.Year.ToString());
            ConfigurationInfo ci = list.GetConfigurationInfo(Service_Date.Month.ToString(), Service_Date.Year.ToString());
            if (ci == null) //Do not set a close date
                return false;
            else
            {
                if (DateTime.Today > ci.ClosingDate) // Closing date is over/no closing date is set
                    return false;
                else
                    return true;
            }
        }

        #endregion

        #region Factory Methods
        protected Assistance()
        { /* require use of factory methods */ }

        internal static Assistance NewAssistance()
        {

            return DataPortal.CreateChild<Assistance>();
        }


        public static Assistance GetAssistence(SafeDataReader dr)
        {
            return DataPortal.FetchChild<Assistance>(dr);
        }

        public static Assistance CreateNew()
        {

            return NewAssistance();
        }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {

            using (BypassPropertyChecks)
            {
                this.ObjId = -1;
                this.LocationAudiences = LocationAudiences.NewLocationAudiences();
                this.IncreaseStudentPerformances = IncreaseStudentPerformances.NewIncreaseStudentPerformances();
                this.PostSecondaryCredits = PostSecondaryCredits.NewPostSecondaryCredits();
                this.FinancialIntegritys = FinancialIntegritys.NewFinancialIntegritys();
                this.NonFirstStandardFinancialAssistances = NonFirstStandardFinancialAssistances.NewNonFirstStandardFinancialAssistances();
                this.StrategicPrioritys = StrategicPrioritys.NewStrategicPrioritys(); // Added by VM 6-19-2018
            }
        }

        protected void MapFieldsFromData(SafeDataReader dr)
        {
            using (BypassPropertyChecks)
            {
                LoadProperty(ObjIdProperty, dr.GetInt32("obj_id"));
                LoadProperty(User_IdProperty, dr.GetInt32("user_id"));
                LoadProperty(SidProperty, dr.GetGuid("sid"));
                LoadProperty(Room_IdProperty, dr.GetInt32("room_id"));
                LoadProperty(Room_NameProperty, dr.GetString("specific_site"));
                LoadProperty(Site_IdProperty, dr.GetInt32("site_id"));
                LoadProperty(SiteType_IdProperty, dr.GetInt32("site_type_id"));
                LoadProperty(Site_NameProperty, dr.GetString("client"));
                LoadProperty(Org_IdProperty, dr.GetInt32("org_id"));
                LoadProperty(Focus_IdProperty, dr.GetInt32("focus_id"));
                LoadProperty(Contact_IdProperty, dr.GetInt32("contact_id"));
                LoadProperty(Activity_IdProperty, dr.GetInt32("activity_id"));
                LoadProperty(Activity_NameProperty, dr.GetString("activity"));
                LoadProperty(Service_Hour_IdProperty, dr.GetInt32("service_hour_id"));
                LoadProperty(Service_LengthProperty, dr.GetDouble("totHours"));
                LoadProperty(Service_DateProperty, dr.GetSmartDate("date", true));
                LoadProperty(Budget_IdProperty, dr.GetInt32("budget_id"));
                LoadProperty(TEC_Purpose_IdProperty, dr.GetInt32("tec_purpose_id"));
                LoadProperty(CommentsProperty, dr.GetString("comments"));
                LoadProperty(FundingIdProperty, dr.GetInt32("funding_id"));
                LoadProperty(IsSchoolFinanceRelatedProperty, dr.GetBoolean("isSchoolFinanceRelated"));
                LoadProperty(IsExtendedLearningOpportunitiesProperty, dr.GetBoolean("ExtendedLearningOpportunities"));

                LoadProperty(IsCoreServiceProperty, dr.GetBoolean("CoreService"));
                LoadProperty(Is22bMassCommunicationProperty, dr.GetBoolean("is22bMassCommunication")); //Added by VM 10-2-2018

                //LoadProperty(Focus_NameProperty, dr.GetString("point"));
                //LoadProperty(Team_IdProperty, dr.GetInt32("team_id"));
                //LoadProperty(Department_IdProperty, dr.GetInt32("department_id"));
                //LoadProperty(Division_IdProperty, dr.GetInt32("division_id"));                
                //LoadProperty(Service_LengthProperty, dr.GetDouble("totHours"));
                //LoadProperty(Service_Hour_AmountProperty, dr.GetDouble("service_hour_amount"));
                //LoadProperty(Travel_Hour_AmountProperty, dr.GetDouble("travel_hour_amount"));                
                //LoadProperty(Activity_NameProperty, dr.GetString("activity"));                
                // LoadProperty(Travel_Hour_IdProperty, dr.GetInt32("travel_hour_id"));
                // LoadProperty(Cross_Divisional_FocusProperty, dr.GetBoolean("cross_divisional_focus"));                
                //LoadProperty(ESC_Strand_IdProperty, dr.GetInt32("esc_strand_id"));
                //LoadProperty(ESC_U_Competency_IdProperty, dr.GetInt32("esc_u_competency_id"));
                //LoadProperty(Manager_SidProperty, dr.GetGuid("manager_sid"));                
                //LoadProperty(ECampusProperty, dr.GetString("ecampus"));                 
                //LoadProperty(IsCoreServiceProperty, dr.GetBoolean("CoreService"));
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected void Child_Update(Assistances a)
        {

            if (this.IsSelfDirty)
            {
                using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        // Update activity info
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "ItsAssistancesUpdate";
                        cm.Parameters.AddWithValue("@obj_id", this.ObjId);
                        this.MapFieldsToParams(cm);
                        cm.ExecuteNonQuery();
                    }
                }
            }
            this.DataPortal_Update();

        }

        protected void Child_Insert(Assistances a)
        {
            if (this.IsNew)
            {

                using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ItsAssistancesInsert";
                        cmd.Parameters.AddWithValue("@obj_id", this.ObjId).Direction = ParameterDirection.Output;
                        this.MapFieldsToParams(cmd);
                        cmd.ExecuteNonQuery();
                        this.ObjId = Convert.ToInt32(cmd.Parameters["@obj_id"].Value);
                    }
                    this.DataPortal_Update();
                }
            }
        }

        protected void Child_DeleteSelf(Assistances a)
        {

            if (this.IsDeleted)
            {
                using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ItsAssistancesDelete";
                        cmd.Parameters.AddWithValue("@obj_id", this.ObjId);
                        cmd.ExecuteNonQuery();
                    }
                    this.DataPortal_Update();
                }
            }
        }

        private void Child_Fetch(SafeDataReader dr)
        {
            MapFieldsFromData(dr);
            LoadProperty(LocationAudiencesProperty, LocationAudiences.GeLocationAudiences(dr.GetInt32("obj_id")));
            LoadProperty(IncreaseStudentPerformancesProperty, IncreaseStudentPerformances.GetIncreaseStudentPerformances(dr.GetInt32("obj_id")));
            LoadProperty(PostSecondaryCreditsProperty, PostSecondaryCredits.GetPostSecondaryCredits(dr.GetInt32("obj_id")));
            LoadProperty(FinancialIntegritysProperty, FinancialIntegritys.GetFinancialIntegritys(dr.GetInt32("obj_id")));
            LoadProperty(NonFirstStandardFinancialAssistancesProperty, NonFirstStandardFinancialAssistances.GetNonFirstStandardFinancialAssistances(dr.GetInt32("obj_id")));
            LoadProperty(StrategicPrioritysProperty, StrategicPrioritys.GetStrategicPrioritys(dr.GetInt32("obj_id"))); // Added by VM 6-19-2018

        }

        protected void MapFieldsToParams(SqlCommand cmd)
        {
            //cmd.Parameters.AddWithValue("@obj_id", ObjId);
            cmd.Parameters.AddWithValue("user_id", User_Id);
            cmd.Parameters.AddWithValue("room_id", Room_Id);
            cmd.Parameters.AddWithValue("site_id", Site_Id);
            cmd.Parameters.AddWithValue("org_id", Org_Id);
            cmd.Parameters.AddWithValue("focus_id", Focus_Id);
            cmd.Parameters.AddWithValue("service_hour_id", Service_Hour_Id);
            cmd.Parameters.AddWithValue("contact_id", Contact_Id);
            cmd.Parameters.AddWithValue("activity_id", Activity_Id);
            cmd.Parameters.AddWithValue("date", Service_Date);
            cmd.Parameters.AddWithValue("budget_id", Budget_Id);

            cmd.Parameters.AddWithValue("comments", Comments);
            cmd.Parameters.AddWithValue("isSchoolFinanceRelated", IsSchoolFinanceRelated);
            cmd.Parameters.AddWithValue("ExtendedLearningOpportunities", IsExtendedLearningOpportunities);
            cmd.Parameters.AddWithValue("CoreService", IsCoreService);
            cmd.Parameters.AddWithValue("funding_id", FundingId);
            cmd.Parameters.AddWithValue("tecpurpose", TEC_Purpose_Id);
            cmd.Parameters.AddWithValue("is22bMassCommunication", Is22bMassCommunication); //Added by VM 10-2-2018
                                                                                           //cmd.Parameters.AddWithValue("team_id", Team_Id);
                                                                                           //cmd.Parameters.AddWithValue("ecampus", ECampus);
                                                                                           //cmd.Parameters.AddWithValue("department_id", Department_Id);
                                                                                           //cmd.Parameters.AddWithValue("division_id", Division_Id);
                                                                                           //cmd.Parameters.AddWithValue("travel_hour_id", Travel_Hour_Id);
                                                                                           //cmd.Parameters.AddWithValue("cross_divisional_focus", Cross_Divisional_Focus);            
                                                                                           //cmd.Parameters.AddWithValue("esc_strand_id", ESC_Strand_Id);
                                                                                           //cmd.Parameters.AddWithValue("esc_u_competency_id", ESC_U_Competency_Id);
                                                                                           //cmd.Parameters.AddWithValue("manager_sid", Manager_Sid);

        }

        public double GetHours(int id)
        {
            double amount = 0.0;

            using (SqlConnection cn = region4.Common.DataConnection.DbConnection)
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ItsHoursAmountGet";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@amount", amount).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    amount = Convert.ToDouble(cmd.Parameters["@amount"].Value);
                }

            }

            return amount;
        }


        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (SqlConnection dbConn = region4.Common.DataConnection.DbConnection)
            {
                dbConn.Open();

                //need to add to this later
                if ((FieldManager.FieldExists(LocationAudiencesProperty) && this.LocationAudiences.IsDirty)
                    || (FieldManager.FieldExists(FinancialIntegritysProperty) && this.FinancialIntegritys.IsDirty)
                    || (FieldManager.FieldExists(IncreaseStudentPerformancesProperty) && this.IncreaseStudentPerformances.IsDirty)
                    || (FieldManager.FieldExists(PostSecondaryCreditsProperty) && this.PostSecondaryCredits.IsDirty)
                    || (FieldManager.FieldExists(NonFirstStandardFinancialAssistancesProperty) && this.NonFirstStandardFinancialAssistances.IsDirty)
                    || (FieldManager.FieldExists(StrategicPrioritysProperty) && this.StrategicPrioritys.IsDirty)) // Added by VM 6-19-2018
                {
                    FieldManager.UpdateChildren(this, dbConn);
                }
            }
        }

        #endregion
    }
}
