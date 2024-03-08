namespace SQNBack.Utils
{
    public enum SQNErrorCode
    {
        //0 When all goes well
        None = 0,

        //1000 When the value is requires
        NullValue = 1000,
        MissingName = 1001,
        MissingLastname = 1002,
        MissingEmail = 1003,
        MissingPhoneNumber = 1004,
        MissingStatus = 1005,
        MissingDocumentType = 1006,
        MissingAssociatedValue = 1007,
        MissingLocation = 1008,
        MissingCode = 1009,
        MissingObservation = 1010,
        MissingAddress = 1011,
        MissingUsername = 1012,
        MissingPassword = 1013,
        MissingIcono = 1014,
        MissingFileName = 1015,
        MissingComments = 1016,
        MediaNotFound = 1017,
        MissingIdNumber = 1018,
        MissingTonnage = 1019,
        MissingSoat = 1020,
        MissingRCC = 1021,
        MissingRCE = 1022,
        MissingNIT = 1023,
        MissingTechMechReview = 1024,
        MissingAlcoholLevel = 1025,
        MissingReport = 1026,
        MissingGroup = 1027,
        MissingRanges = 1028,
        MissingArea = 1029,

        //2000 When the searched value wasn't found
        ErrorMessageNotFound = 2000,
        StatusNotFound = 2001,
        RangeNotFound = 2002,
        ReportsNotFound = 2003,
        RoleNotFound = 2004,
        CustomerNotFound = 2005,
        ExpertNotFound = 2006,
        AreaNotFound = 2007,
        ClassificationNotFound = 2008,
        ServiceNotFound = 2009,
        UserNotFound = 2010,
        DataElementNotFound = 2011,

        //3000 When the value Exist in the system
        ErrorMessageAlreadyExist = 3000,
        EmailAlreadyExist = 3001,
        PersonAlreadyExist = 3002,
        RangeAlreadyExist = 3003,
        CellPhoneAlreadyExisit = 3004,
        AreaAlreadyExist = 3005,
        ClassificationAlreadyExist = 3006,
        RoleNameAlreadyExist = 3007,
        ExpertAlreadyExist = 3008,
        DataElementAlreadyExist = 3009,
        ServiceAlreadyExist = 3010,
        StatusAlreadyExist = 3011,
        UserAlreadyExist = 3012,
        InvolvedAlreadyExist = 3013,

        //5000 Validation eroors
        ValidationError = 5000,
        OrderError = 5001,
        AreaError = 5002,
        InvalidEmail = 5003,
        ValueMustBeUpper = 5004,
        WrongCalificationValue = 5005,
        NoResult = 5006,
        TooManyValues = 5007,
        StatusIsNoValid = 5008,
        ExpertNotValid = 5009,
        UpdateIdNotMatch = 5010,
        NotMatchingValues = 5011,
        StatusNotForCreation = 5012,

        //6000 System errors
        SystemError = 6000,
        NetworkError = 6001
    }
}