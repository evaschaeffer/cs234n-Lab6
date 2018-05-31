using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsCSharp
{
    /// <summary>
    /// BaseBusiness is the class from which all business classes will be derived.
    /// Derived classes must implement constructors and property procedures as well as
    /// the 2 abstract methods SetRequiredRules and SetDefaultProperties.
    /// This class relies on the implementation of corresponding property and data access classes.
    /// </summary>
    public abstract class BaseBusiness
    {
        // these methods are called by the constructor.  The base class can't know how to 
        // implement them BUT derived classes must write them so that the contructor can call them.
        protected abstract void SetRequiredRules();
        protected abstract void SetDefaultProperties();
        protected abstract void SetUp();

        public abstract object GetList();

        // serializable properties of the object
        // extra copy so that the user can "undo" editing changes before they're sent to the db
        protected IBaseProps mProps;
        protected IBaseProps mOldProps;

        // data tier objects
        protected IReadDB mdbReadable;
        protected IWriteDB mdbWriteable;

        // editing state of the object
        protected bool mIsNew = true;
        protected bool mIsDeleted = false;
        protected bool mIsDirty = false;

        // collection of business rules that are currently "broken"
        // used to determine if its safe to save the object to the databse
        protected BrokenRules mRules = new BrokenRules();

        // desktop apps generally retrieve connection string information from the config file when the user logs in
        // db objects don't have to get the connection string themselves if the ui objects pass it as a parameter
        // in the call to the constructor
        protected string mConnectionString = "";

        // creates a new business object
        public BaseBusiness()
        {
            SetUp();
            SetRequiredRules();
            SetDefaultProperties();
        }

        public BaseBusiness(string cnString)
        {
            mConnectionString = cnString;
            SetUp();
            SetRequiredRules();
            SetDefaultProperties();
        }

        // all of the constructors that take a key as the first parameter
        // retrieve the business object from the data store based on it's key
        public BaseBusiness(object key, string cnString)
        {
            mConnectionString = cnString;
            SetUp();
            Load(key);
        }

        public BaseBusiness(object key)
        {
            SetUp();
            Load(key);
        }

        // these 2 constructors assume that you have a properties object
        public BaseBusiness(IBaseProps props, string cnString)
        {
            mConnectionString = cnString;
            SetUp();
            LoadProps(props);
        }

        public BaseBusiness(IBaseProps props)
        {
            SetUp();
            LoadProps(props);
        }

        // editing state of the business object
        public bool IsNew
        {
            get
            {
                return mIsNew;
            }

        }

        public bool IsDeleted
        {
            get
            {
                return mIsDeleted;
            }
        }

        public virtual bool IsDirty
        {
            get
            {
                return mIsDirty;
            }
        }

        public bool IsValid
        {
            get
            {
                return (mRules.Count == 0);
            }
        }

        public override string ToString()
        {
            return mProps.GetState();
        }

        public virtual void UndoChanges()
        {
            if (mIsDirty || mIsDeleted)
            {
                mProps = (IBaseProps)mOldProps.Clone();
                mIsDirty = false;
                mIsDeleted = false;
                if (mIsNew)
                    SetRequiredRules();
                else
                    mRules.Clear();
            }
        }

        // loads the object from the database based on it's key
        public virtual void Load(Object key)
        {
            mProps = mdbReadable.Retrieve(key);
            mOldProps = (IBaseProps)mProps.Clone();

            mIsDirty = false;
            mIsNew = false;
            mIsDeleted = false;

            mRules.Clear();
        }
        
        // loads from an xml string
        public virtual void LoadXML(string xml)
        {
            mProps.SetState(xml);
            mOldProps = (IBaseProps)mProps.Clone();

            mIsDirty = false;
            mIsNew = false;
            mIsDeleted = false;

            mRules.Clear();
        }

        // loads from a properties object
        public virtual void LoadProps(IBaseProps props)
        {
            mProps = (IBaseProps)props.Clone();
            mOldProps = (IBaseProps)props.Clone();

            mIsDirty = false;
            mIsNew = false;
            mIsDeleted = false;

            mRules.Clear();

        }

        // saves to a data store
        public virtual void Save()
        {
            if (mIsDeleted && !mIsNew)
            {
                if (mdbWriteable.Delete(mProps))
                {
                    mIsDeleted = false;
                    mIsNew = true;
                    mIsDirty = false;
                    SetRequiredRules();
                    SetDefaultProperties();
                    mOldProps = (IBaseProps)mProps.Clone();
                }
            }
            else if (mIsDeleted && mIsNew)
            {
                mIsDeleted = false;
                mIsNew = true;
                mIsDirty = false;
                SetRequiredRules();
                SetDefaultProperties();
                mOldProps = (IBaseProps)mProps.Clone();
            }
            else if (!IsValid)
            {
                string message;
                if (mRules.Count == 1)
                {
                    message = "Object cannot be saved. One property is invalid.";
                }
                else
                {
                    message = "Object can not be saved. " + mRules.Count + " properties are invalid.";
                }
                throw new Exception(message);
            }
            else if (mIsNew && !mIsDeleted)
            {
                mProps = mdbWriteable.Create(mProps);
                mIsNew = false;
                mIsDirty = false;
                mIsDeleted = false;
                mRules.Clear();
                mOldProps = (IBaseProps)mProps.Clone();
            }
            else if (IsDirty)
            {
                if (mdbWriteable.Update(mProps))
                {
                    mIsDirty = false;
                    mIsNew = false;
                    mIsDeleted = false;
                    mRules.Clear();
                    mOldProps = (IBaseProps)mProps.Clone();
                }
            } // end logic related to editing status
        }// end Save

        /// <summary>
        /// This method only marks the object for deletion. To actually delete it from the DB, 
        /// you must call Save() after calling Delete().
        /// </summary>
        public void Delete()
        {
            mIsDeleted = true;
        }
    }
}
