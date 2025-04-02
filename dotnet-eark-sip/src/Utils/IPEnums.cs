namespace IPEnums
{
    /// <summary>
    /// Represents the type of Information Package (IP).
    /// </summary>
    public enum IPType
    {
        /// <summary>
        /// Submission Information Package.
        /// </summary>
        SIP,
        /// <summary>
        /// Archival Information Package.
        /// </summary>
        AIP,
        /// <summary>
        /// Dissemination Information Package.
        /// </summary>
        DIP,
        /// <summary>
        /// SIARD Information Package.
        /// </summary>
        SIARD
    }

    /// <summary>
    /// Represents the status of an Information Package (IP).
    /// </summary>
    public enum IPStatus
    {
        /// <summary>
        /// A new Information Package.
        /// </summary>
        NEW,
        /// <summary>
        /// An updated Information Package.
        /// </summary>
        UPDATE,
        /// <summary>
        /// A replacement Information Package.
        /// </summary>
        REPLACEMENT,
        /// <summary>
        /// A test Information Package.
        /// </summary>
        TEST
    }

    /// <summary>
    /// Represents the type of Submission Information Package (SIP).
    /// </summary>
    public enum SIPType
    {
        /// <summary>
        /// E-ARK 2 SIP with schema version S.
        /// </summary>
        EARK2S,
        /// <summary>
        /// E-ARK 2 SIP.
        /// </summary>
        EARK2,
        /// <summary>
        /// SIARD SIP.
        /// </summary>
        SIARD,
        /// <summary>
        /// ERMS SIP.
        /// </summary>
        ERMS
    }
}