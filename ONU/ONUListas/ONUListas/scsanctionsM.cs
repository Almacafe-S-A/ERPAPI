<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
=======
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

namespace ONUListas
{
    public class CONSOLIDATED_LISTM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }
        public List<INDIVIDUALM> iNDIVIDUALS { get; set; }

        public List<ENTITYM> eNTITIES { get; set; }
=======
       public int Id { get; set; }
        public List<INDIVIDUALM> iNDIVIDUALS { get; set; }

        public List<ENTITYM> ENTITIES { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

        public System.DateTime dateGenerated { get; set; }
    }



    public partial class INDIVIDUALM
    {
        [Key]
        public int Id { get; set; }

<<<<<<< HEAD
        public string dATAIDField { get; set; }

        public string vERSIONNUMField { get; set; }

        public string fIRST_NAMEField { get; set; }

        public string sECOND_NAMEField { get; set; }

        public string tHIRD_NAMEField { get; set; }

        public string fOURTH_NAMEField { get; set; }

        public string uN_LIST_TYPEField { get; set; }

        public string rEFERENCE_NUMBERField { get; set; }

        public System.DateTime lISTED_ONField { get; set; }

        public string gENDERField { get; set; }

        public string sUBMITTED_BYField { get; set; }

        public string nAME_ORIGINAL_SCRIPTField { get; set; }

        public string cOMMENTS1Field { get; set; }

        public string nATIONALITY2Field { get; set; }

        public string tITLEField { get; set; }

        public string dESIGNATIONField { get; set; }

        public string nATIONALITYField { get; set; }

        public LIST_TYPEM lIST_TYPEField { get; set; }

        public string lAST_DAY_UPDATEDField { get; set; }

        public List<INDIVIDUAL_ALIASM> iNDIVIDUAL_ALIASField { get; set; }

        public List<INDIVIDUAL_ADDRESSM> iNDIVIDUAL_ADDRESSField { get; set; }

        public List<INDIVIDUAL_DATE_OF_BIRTHM> iNDIVIDUAL_DATE_OF_BIRTHField { get; set; }

        public List<INDIVIDUAL_PLACE_OF_BIRTHM> iNDIVIDUAL_PLACE_OF_BIRTHField { get; set; }

        public List<INDIVIDUAL_DOCUMENTM> iNDIVIDUAL_DOCUMENTField { get; set; }

        public string sORT_KEYField { get; set; }

        public string sORT_KEY_LAST_MODField { get; set; }

        public System.DateTime dELISTED_ONField { get; set; }

        public bool dELISTED_ONFieldSpecified { get; set; }
=======
        public string DATAID { get; set; }

        public string VERSIONNUM { get; set; }

        public string FIRST_NAME { get; set; }

        public string SECOND_NAME { get; set; }

        public string THIRD_NAME { get; set; }

        public string FOURTH_NAME { get; set; }

        public string UN_LIST_TYPE { get; set; }

        public string REFERENCE_NUMBER { get; set; }

        public System.DateTime LISTED_ON { get; set; }

        public string GENDER { get; set; }

        public string SUBMITTED_BY { get; set; }

        public string NAME_ORIGINAL_SCRIPT { get; set; }

        public string COMMENTS1 { get; set; }

        public string NATIONALITY2 { get; set; }

        public string TITLE { get; set; }

        public string DESIGNATION { get; set; }

        public string NATIONALITY { get; set; }

        public LIST_TYPEM LIST_TYPE { get; set; }

        public string LAST_DAY_UPDATED { get; set; }

        public List<INDIVIDUAL_ALIASM> INDIVIDUAL_ALIAS { get; set; }

        public List<INDIVIDUAL_ADDRESSM> INDIVIDUAL_ADDRESS { get; set; }

        public List<INDIVIDUAL_DATE_OF_BIRTHM> INDIVIDUAL_DATE_OF_BIRTH { get; set; }

        public List<INDIVIDUAL_PLACE_OF_BIRTHM> INDIVIDUAL_PLACE_OF_BIRTH { get; set; }

        public List<INDIVIDUAL_DOCUMENTM> INDIVIDUAL_DOCUMENT { get; set; }

        public string SORT_KEY { get; set; }

        public string SORT_KEY_LAST_MOD { get; set; }

        public System.DateTime DELISTED_ON { get; set; }

        public bool DELISTED_ONSpecified { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }



    public partial class LIST_TYPEM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string vALUEField { get; set; }
=======
         public int Id { get; set; }

        public string VALUE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }


    public partial class INDIVIDUAL_ALIASM
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string qUALITYField { get; set; }

        public string aLIAS_NAMEField { get; set; }

        public string dATE_OF_BIRTHField { get; set; }

        public string cITY_OF_BIRTHField { get; set; }

        public string cOUNTRY_OF_BIRTHField { get; set; }

        public string nOTEField { get; set; }
=======
        public string QUALITY { get; set; }

        public string ALIAS_NAME { get; set; }

        public string DATE_OF_BIRTH { get; set; }

        public string CITY_OF_BIRTH { get; set; }

        public string COUNTRY_OF_BIRTH { get; set; }

        public string NOTE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }

    public partial class INDIVIDUAL_ADDRESSM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string sTREETField { get; set; }

        public string cITYField { get; set; }

        public string sTATE_PROVINCEField { get; set; }

        public string zIP_CODEField { get; set; }

        public string cOUNTRYField { get; set; }

        public string nOTEField { get; set; }
=======
         public int Id { get; set; }

        public string STREET { get; set; }

        public string CITY { get; set; }

        public string STATE_PROVINCE { get; set; }

        public string ZIP_CODE { get; set; }

        public string COUNTRY { get; set; }

        public string NOTE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }


    public partial class INDIVIDUAL_DATE_OF_BIRTHM
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string tYPE_OF_DATEField { get; set; }

        public string nOTEField { get; set; }

        public object itemsField { get; set; }

        public string itemsElementNameField { get; set; }
=======
        public string TYPE_OF_DATE { get; set; }

        public string NOTE { get; set; }

        public string Items { get; set; }

        public string ItemsElementName { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }

    public partial class INDIVIDUAL_PLACE_OF_BIRTHM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string cITYField { get; set; }

        public string sTATE_PROVINCEField { get; set; }

        public string cOUNTRYField { get; set; }

        public string nOTEField { get; set; }
=======
         public int Id { get; set; }

        public string CITY { get; set; }

        public string STATE_PROVINCE { get; set; }

        public string COUNTRY { get; set; }

        public string NOTE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }

        public partial class INDIVIDUAL_DOCUMENTM
    {
        [Key]
        public int Id { get; set; }

<<<<<<< HEAD
        public string tYPE_OF_DOCUMENTField { get; set; }

        public string tYPE_OF_DOCUMENT2Field { get; set; }

        public string nUMBERField { get; set; }

        public string iSSUING_COUNTRYField { get; set; }

        public string dATE_OF_ISSUEField { get; set; }

        public string cITY_OF_ISSUEField { get; set; }

        public string cOUNTRY_OF_ISSUEField { get; set; }

        public string nOTEField { get; set; }
=======
        public string TYPE_OF_DOCUMENT { get; set; }

        public string TYPE_OF_DOCUMENT2 { get; set; }

        public string NUMBER { get; set; }

        public string ISSUING_COUNTRY { get; set; }

        public string DATE_OF_ISSUE { get; set; }

        public string CITY_OF_ISSUE { get; set; }

        public string COUNTRY_OF_ISSUE { get; set; }

        public string NOTE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

    }

    public partial class ENTITYM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string dATAIDField { get; set; }

        public string vERSIONNUMField { get; set; }

        public string fIRST_NAMEField { get; set; }

        public string uN_LIST_TYPEField { get; set; }

        public string rEFERENCE_NUMBERField { get; set; }

        public System.DateTime lISTED_ONField { get; set; }

        public System.DateTime sUBMITTED_ONField { get; set; }

        public bool sUBMITTED_ONFieldSpecified { get; set; }

        public string nAME_ORIGINAL_SCRIPTField { get; set; }

        public string cOMMENTS1Field { get; set; }

        public LIST_TYPEM lIST_TYPEField { get; set; }

        public string lAST_DAY_UPDATEDField { get; set; }

        public List<ENTITY_ALIASM> eNTITY_ALIASField { get; set; }

        public List<ENTITY_ADDRESSM> eNTITY_ADDRESSField { get; set; }

        public string sORT_KEYField { get; set; }

        public string sORT_KEY_LAST_MODField { get; set; }

        public System.DateTime dELISTED_ONField { get; set; }

        public bool dELISTED_ONFieldSpecified{ get; set; }
=======
      public int Id { get; set; }

        public string DATAID { get; set; }

        public string VERSIONNUM { get; set; }

        public string FIRST_NAME { get; set; }

        public string UN_LIST_TYPE { get; set; }

        public string REFERENCE_NUMBER { get; set; }

        public System.DateTime LISTED_ON { get; set; }

        public System.DateTime SUBMITTED_ON { get; set; }

        public bool SUBMITTED_ONSpecified { get; set; }

        public string NAME_ORIGINAL_SCRIPT { get; set; }

        public string COMMENTS1 { get; set; }

        public LIST_TYPEM LIST_TYPE { get; set; }

        public string LAST_DAY_UPDATED { get; set; }

        public List<ENTITY_ALIASM> ENTITY_ALIAS { get; set; }

        public List<ENTITY_ADDRESSM> ENTITY_ADDRESS { get; set; }

        public string SORT_KEY { get; set; }

        public string SORT_KEY_LAST_MOD { get; set; }

        public System.DateTime DELISTED_ON { get; set; }

        public bool DELISTED_ONSpecified { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

    }



    public partial class ENTITY_ALIASM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string qUALITYField { get; set; }

        public string aLIAS_NAMEField { get; set; }
=======
         public int Id { get; set; }

        public string QUALITY { get; set; }

        public string ALIAS_NAME { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }

    public partial class ENTITY_ADDRESSM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string sTREETField { get; set; }

        public string cITYField { get; set; }

        public string sTATE_PROVINCEField { get; set; }

        public string zIP_CODEField { get; set; }

        public string cOUNTRYField { get; set; }

        public string nOTEField { get; set; }
=======
       public int Id { get; set; }

        public string STREET { get; set; }

        public string CITY { get; set; }

        public string STATE_PROVINCE { get; set; }

        public string ZIP_CODE { get; set; }

        public string COUNTRY { get; set; }

        public string NOTE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }


    public partial class INDIVIDUALSM
    {
        [Key]
        public int Id { get; set; }

<<<<<<< HEAD
        public List<INDIVIDUALM> iNDIVIDUALField { get; set; }
=======
        public List<INDIVIDUALM> INDIVIDUAL { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }

    public partial class TITLEM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string vALUEField { get; set; }
=======
       public int Id { get; set; }

        public string VALUE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }


    public partial class DESIGNATIONM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string vALUEField { get; set; }
=======
       public int Id { get; set; }

        public string VALUE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }
    public partial class NATIONALITYM
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        public string vALUEField { get; set; }
=======
      public int Id { get; set; }

        public string VALUE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f
    }


    public partial class ENTITIESM
    {
<<<<<<< HEAD
        [Key]
        public int Id { get; set; }

        public List<ENTITYM> eNTITYField { get; set; }
=======
       
        [Key]
       public int Id { get; set; }

        public List<ENTITYM> ENTITY { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

    }

    public partial class LAST_DAY_UPDATEDM
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string vALUEField { get; set; }
=======
        public string VALUE { get; set; }
>>>>>>> 42b2fb91e01bba1acf16e6738e453b9f4c701f0f

    }



    }
