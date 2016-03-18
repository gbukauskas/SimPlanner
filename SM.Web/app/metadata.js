(function(){	window.medsimMetadata = {
		getBreezeMetadata: getBreezeMetadata,
		getBreezeValidators: getBreezeValidators
	}
	function getBreezeMetadata(){
		return JSON.stringify({"metadataVersion":"1.0.5","namingConvention":"camelCase","localQueryComparisonOptions":"caseInsensitiveSQL","dataServices":[],"structuralTypes":[{"shortName":"CountryDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Countries","dataProperties":[{"name":"code","dataType":"String","isNullable":false,"defaultValue":"","isPartOfKey":true,"maxLength":2,"validators":[{"name":"required"},{"name":"stringLength","minLength":2,"maxLength":2}]},{"name":"name","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]},{"name":"dialCode","dataType":"String","isNullable":false,"defaultValue":"","maxLength":3,"validators":[{"name":"required"},{"name":"stringLength","minLength":2,"maxLength":3},{"name":"regularExpression","expression":"\\d+"}]}],"navigationProperties":[{"name":"countryLocales","entityTypeName":"CountryLocaleCodeDto:#SM.Dto","isScalar":false,"associationName":"CountryDto_CountryLocales","invForeignKeyNames":["countryCode"]},{"name":"hospitals","entityTypeName":"InstitutionDto:#SM.Dto","isScalar":false,"associationName":"InstitutionDto_Country","invForeignKeyNames":["countryCode"]}]},{"shortName":"CountryLocaleCodeDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CountryLocales","dataProperties":[{"name":"countryCode","dataType":"String","isNullable":false,"defaultValue":"","isPartOfKey":true,"maxLength":2,"validators":[{"name":"required"},{"maxLength":2,"name":"maxLength"}]},{"name":"localeCode","dataType":"String","isNullable":false,"defaultValue":"","isPartOfKey":true,"maxLength":5,"validators":[{"name":"required"},{"name":"stringLength","minLength":5,"maxLength":5},{"name":"regularExpression","expression":"[a-z]{2}-[A-Z]{2}"}]},{"name":"preference","dataType":"Byte","isNullable":false,"defaultValue":0,"isPartOfKey":true,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]}],"navigationProperties":[{"name":"country","entityTypeName":"CountryDto:#SM.Dto","isScalar":true,"associationName":"CountryDto_CountryLocales","foreignKeyNames":["countryCode"]}]},{"shortName":"InstitutionDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Institutions","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"name","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]},{"name":"about","dataType":"String","validators":[{"name":"string"}]},{"name":"countryCode","dataType":"String","maxLength":2,"validators":[{"name":"stringLength","minLength":2,"maxLength":2}]}],"navigationProperties":[{"name":"country","entityTypeName":"CountryDto:#SM.Dto","isScalar":true,"associationName":"InstitutionDto_Country","foreignKeyNames":["countryCode"]},{"name":"departments","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":false,"associationName":"InstitutionDto_Departments","invForeignKeyNames":["institutionId"]}]},{"shortName":"DepartmentDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Departments","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"name","dataType":"String","isNullable":false,"defaultValue":"","maxLength":64,"validators":[{"name":"required"},{"maxLength":64,"name":"maxLength"}]},{"name":"abbreviation","dataType":"String","maxLength":16,"validators":[{"maxLength":16,"name":"maxLength"}]},{"name":"institutionId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"invitationLetterFilename","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]},{"name":"certificateFilename","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]}],"navigationProperties":[{"name":"courseParticipants","entityTypeName":"CourseParticipantDto:#SM.Dto","isScalar":false,"associationName":"DepartmentDto_CourseParticipants","invForeignKeyNames":["departmentId"]},{"name":"courses","entityTypeName":"CourseDto:#SM.Dto","isScalar":false,"associationName":"DepartmentDto_Courses","invForeignKeyNames":["departmentId"]},{"name":"institution","entityTypeName":"InstitutionDto:#SM.Dto","isScalar":true,"associationName":"InstitutionDto_Departments","foreignKeyNames":["institutionId"]},{"name":"manequins","entityTypeName":"ManequinDto:#SM.Dto","isScalar":false,"associationName":"ManequinDto_Department","invForeignKeyNames":["departmentId"]},{"name":"outreachCourses","entityTypeName":"CourseDto:#SM.Dto","isScalar":false,"associationName":"DepartmentDto_OutreachCourses","invForeignKeyNames":["outreachingDepartmentId"]},{"name":"participants","entityTypeName":"ParticipantDto:#SM.Dto","isScalar":false,"associationName":"DepartmentDto_Participants","invForeignKeyNames":["defaultDepartmentId"]},{"name":"rooms","entityTypeName":"RoomDto:#SM.Dto","isScalar":false,"associationName":"DepartmentDto_Rooms","invForeignKeyNames":["departmentId"]},{"name":"scenarios","entityTypeName":"ScenarioDto:#SM.Dto","isScalar":false,"associationName":"DepartmentDto_Scenarios","invForeignKeyNames":["departmentId"]}]},{"shortName":"CourseParticipantDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CourseParticipants","dataProperties":[{"name":"courseId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"participantId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"isConfirmed","dataType":"Boolean","isNullable":false,"defaultValue":false,"validators":[{"name":"required"},{"name":"bool"}]},{"name":"isFaculty","dataType":"Boolean","isNullable":false,"defaultValue":false,"validators":[{"name":"required"},{"name":"bool"}]},{"name":"isOrganiser","dataType":"Boolean","isNullable":false,"defaultValue":false,"validators":[{"name":"required"},{"name":"bool"}]},{"name":"departmentId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"professionalRoleId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"course","entityTypeName":"CourseDto:#SM.Dto","isScalar":true,"associationName":"CourseDto_CourseParticipants","foreignKeyNames":["courseId"]},{"name":"department","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"DepartmentDto_CourseParticipants","foreignKeyNames":["departmentId"]},{"name":"participant","entityTypeName":"ParticipantDto:#SM.Dto","isScalar":true,"associationName":"ParticipantDto_CourseParticipants","foreignKeyNames":["participantId"]},{"name":"professionalRole","entityTypeName":"ProfessionalRoleDto:#SM.Dto","isScalar":true,"associationName":"ProfessionalRoleDto_CourseParticipants","foreignKeyNames":["professionalRoleId"]}]},{"shortName":"CourseDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Courses","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"startTime","dataType":"DateTime","isNullable":false,"defaultValue":"","validators":[{"name":"required"},{"name":"date"}]},{"name":"facultyMeetingTime","dataType":"DateTime","validators":[{"name":"date"}]},{"name":"departmentId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"outreachingDepartmentId","dataType":"Guid","validators":[{"name":"guid"}]},{"name":"roomId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"facultyNoRequired","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]},{"name":"courseTypeId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"participantVideoFilename","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]},{"name":"feedbackSummaryFilename","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]}],"navigationProperties":[{"name":"courseParticipants","entityTypeName":"CourseParticipantDto:#SM.Dto","isScalar":false,"associationName":"CourseDto_CourseParticipants","invForeignKeyNames":["courseId"]},{"name":"courseSlotPresenters","entityTypeName":"CourseSlotPresenterDto:#SM.Dto","isScalar":false,"associationName":"CourseDto_CourseSlotPresenters","invForeignKeyNames":["courseId"]},{"name":"courseType","entityTypeName":"CourseTypeDto:#SM.Dto","isScalar":true,"associationName":"CourseTypeDto_Courses","foreignKeyNames":["courseTypeId"]},{"name":"department","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"DepartmentDto_Courses","foreignKeyNames":["departmentId"]},{"name":"outreachingDepartment","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"DepartmentDto_OutreachCourses","foreignKeyNames":["outreachingDepartmentId"]},{"name":"room","entityTypeName":"RoomDto:#SM.Dto","isScalar":true,"associationName":"RoomDto_Courses","foreignKeyNames":["roomId"]},{"name":"scenarioFacultyRoles","entityTypeName":"ScenarioFacultyRoleDto:#SM.Dto","isScalar":false,"associationName":"CourseDto_ScenarioFacultyRoles","invForeignKeyNames":["courseId"]}]},{"shortName":"CourseSlotPresenterDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CourseSlotPresenters","dataProperties":[{"name":"courseId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"courseSlotId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"presenterId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"course","entityTypeName":"CourseDto:#SM.Dto","isScalar":true,"associationName":"CourseDto_CourseSlotPresenters","foreignKeyNames":["courseId"]},{"name":"courseSlot","entityTypeName":"CourseSlotDto:#SM.Dto","isScalar":true,"associationName":"CourseSlotDto_CourseSlotPresenters","foreignKeyNames":["courseSlotId"]},{"name":"presenter","entityTypeName":"ParticipantDto:#SM.Dto","isScalar":true,"associationName":"ParticipantDto_CourseSlotPresentations","foreignKeyNames":["presenterId"]}]},{"shortName":"CourseSlotDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CourseEvents","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"name","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]},{"name":"minimumFaculty","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]},{"name":"maximumFaculty","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]},{"name":"minutesDuration","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"name":"numericRange","min":1,"max":240}]},{"name":"day","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]},{"name":"order","dataType":"Int32","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":-2147483648,"max":2147483647,"name":"int32"}]}],"navigationProperties":[{"name":"courseSlotPresenters","entityTypeName":"CourseSlotPresenterDto:#SM.Dto","isScalar":false,"associationName":"CourseSlotDto_CourseSlotPresenters","invForeignKeyNames":["courseSlotId"]},{"name":"defaultResources","entityTypeName":"CourseTeachingResourceDto:#SM.Dto","isScalar":false,"associationName":"CourseSlotDto_DefaultResources","invForeignKeyNames":["courseSlotId"]}]},{"shortName":"CourseTypeDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CourseTypes","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"description","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]},{"name":"isInstructorCourse","dataType":"Boolean","isNullable":false,"defaultValue":false,"validators":[{"name":"required"},{"name":"bool"}]},{"name":"daysDuration","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]},{"name":"emersionCategory","dataType":"String","validators":[{"name":"string"}],"enumType":"Edm.Self.Emersion"},{"name":"abbrev","dataType":"String","isNullable":false,"defaultValue":"","maxLength":32,"validators":[{"name":"required"},{"maxLength":32,"name":"maxLength"}]}],"navigationProperties":[{"name":"courses","entityTypeName":"CourseDto:#SM.Dto","isScalar":false,"associationName":"CourseTypeDto_Courses","invForeignKeyNames":["courseTypeId"]},{"name":"scenarios","entityTypeName":"ScenarioDto:#SM.Dto","isScalar":false,"associationName":"CourseTypeDto_Scenarios","invForeignKeyNames":["courseTypeId"]}]},{"shortName":"ScenarioSlotDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CourseScenarios","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"minutesDuration","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"name":"numericRange","min":1,"max":240}]},{"name":"day","dataType":"Byte","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":0,"max":255,"name":"byte"}]},{"name":"order","dataType":"Int32","isNullable":false,"defaultValue":0,"validators":[{"name":"required"},{"min":-2147483648,"max":2147483647,"name":"int32"}]}]},{"shortName":"ScenarioRoleDescriptionDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"SenarioRoles","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"description","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]}],"navigationProperties":[{"name":"scenarioFacultyRoles","entityTypeName":"ScenarioFacultyRoleDto:#SM.Dto","isScalar":false,"associationName":"ScenarioRoleDescriptionDto_ScenarioFacultyRoles","invForeignKeyNames":["roleId"]}]},{"shortName":"ScenarioFacultyRoleDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"ScenarioFacultyRoles","dataProperties":[{"name":"courseId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"scenarioId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"facultyMemberId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"roleId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"course","entityTypeName":"CourseDto:#SM.Dto","isScalar":true,"associationName":"CourseDto_ScenarioFacultyRoles","foreignKeyNames":["courseId"]},{"name":"facultyMember","entityTypeName":"ParticipantDto:#SM.Dto","isScalar":true,"associationName":"ParticipantDto_ScenarioFacultyRoles","foreignKeyNames":["facultyMemberId"]},{"name":"role","entityTypeName":"ScenarioRoleDescriptionDto:#SM.Dto","isScalar":true,"associationName":"ScenarioRoleDescriptionDto_ScenarioFacultyRoles","foreignKeyNames":["roleId"]},{"name":"scenario","entityTypeName":"ScenarioDto:#SM.Dto","isScalar":true,"associationName":"ScenarioDto_ScenarioFacultyRoles","foreignKeyNames":["scenarioId"]}]},{"shortName":"ParticipantDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Participants","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"email","dataType":"String","isNullable":false,"defaultValue":"","maxLength":256,"validators":[{"name":"required"},{"maxLength":256,"name":"maxLength"},{"name":"emailAddress"}]},{"name":"phoneNumber","dataType":"String","maxLength":32,"validators":[{"maxLength":32,"name":"maxLength"},{"name":"phone"}]},{"name":"alternateEmail","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"},{"name":"emailAddress"}]},{"name":"fullName","dataType":"String","isNullable":false,"defaultValue":"","maxLength":256,"validators":[{"name":"required"},{"maxLength":256,"name":"maxLength"}]},{"name":"defaultDepartmentId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"defaultProfessionalRoleId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"courseParticipants","entityTypeName":"CourseParticipantDto:#SM.Dto","isScalar":false,"associationName":"ParticipantDto_CourseParticipants","invForeignKeyNames":["participantId"]},{"name":"courseSlotPresentations","entityTypeName":"CourseSlotPresenterDto:#SM.Dto","isScalar":false,"associationName":"ParticipantDto_CourseSlotPresentations","invForeignKeyNames":["presenterId"]},{"name":"department","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"DepartmentDto_Participants","foreignKeyNames":["defaultDepartmentId"]},{"name":"professionalRole","entityTypeName":"ProfessionalRoleDto:#SM.Dto","isScalar":true,"associationName":"ProfessionalRoleDto_Participants","foreignKeyNames":["defaultProfessionalRoleId"]},{"name":"scenarioFacultyRoles","entityTypeName":"ScenarioFacultyRoleDto:#SM.Dto","isScalar":false,"associationName":"ParticipantDto_ScenarioFacultyRoles","invForeignKeyNames":["facultyMemberId"]}]},{"shortName":"ProfessionalRoleDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"ProfessionalRoles","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"description","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]},{"name":"category","dataType":"String","isNullable":false,"defaultValue":"","validators":[{"name":"required"},{"name":"string"}],"enumType":"Edm.Self.ProfessionalCategory"}],"navigationProperties":[{"name":"courseParticipants","entityTypeName":"CourseParticipantDto:#SM.Dto","isScalar":false,"associationName":"ProfessionalRoleDto_CourseParticipants","invForeignKeyNames":["professionalRoleId"]},{"name":"participants","entityTypeName":"ParticipantDto:#SM.Dto","isScalar":false,"associationName":"ProfessionalRoleDto_Participants","invForeignKeyNames":["defaultProfessionalRoleId"]}]},{"shortName":"ScenarioDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Scenarios","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"description","dataType":"String","isNullable":false,"defaultValue":"","maxLength":128,"validators":[{"name":"required"},{"maxLength":128,"name":"maxLength"}]},{"name":"departmentId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"complexity","dataType":"String","isNullable":false,"defaultValue":"","validators":[{"name":"required"},{"name":"string"}],"enumType":"Edm.Self.Difficulty"},{"name":"emersionCategory","dataType":"String","validators":[{"name":"string"}],"enumType":"Edm.Self.Emersion"},{"name":"templateFilename","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]},{"name":"manequinId","dataType":"Guid","validators":[{"name":"guid"}]},{"name":"courseTypeId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"courseType","entityTypeName":"CourseTypeDto:#SM.Dto","isScalar":true,"associationName":"CourseTypeDto_Scenarios","foreignKeyNames":["courseTypeId"]},{"name":"department","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"DepartmentDto_Scenarios","foreignKeyNames":["departmentId"]},{"name":"manequin","entityTypeName":"ManequinDto:#SM.Dto","isScalar":true,"associationName":"ManequinDto_Scenarios","foreignKeyNames":["manequinId"]},{"name":"resources","entityTypeName":"ScenarioResourceDto:#SM.Dto","isScalar":false,"associationName":"ScenarioResourceDto_Scenario","invForeignKeyNames":["scenarioId"]},{"name":"scenarioFacultyRoles","entityTypeName":"ScenarioFacultyRoleDto:#SM.Dto","isScalar":false,"associationName":"ScenarioDto_ScenarioFacultyRoles","invForeignKeyNames":["scenarioId"]}]},{"shortName":"ManequinDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"Manequins","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"description","dataType":"String","isNullable":false,"defaultValue":"","maxLength":50,"validators":[{"name":"required"},{"maxLength":50,"name":"maxLength"}]},{"name":"departmentId","dataType":"Guid","validators":[{"name":"guid"}]},{"name":"manufacturerId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"department","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"ManequinDto_Department","foreignKeyNames":["departmentId"]},{"name":"manufacturer","entityTypeName":"ManequinManufacturerDto:#SM.Dto","isScalar":true,"associationName":"ManequinManufacturerDto_Manequins","foreignKeyNames":["manufacturerId"]},{"name":"scenarios","entityTypeName":"ScenarioDto:#SM.Dto","isScalar":false,"associationName":"ManequinDto_Scenarios","invForeignKeyNames":["manequinId"]}]},{"shortName":"ManequinManufacturerDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"ManequinManufacturers","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"name","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]}],"navigationProperties":[{"name":"manequins","entityTypeName":"ManequinDto:#SM.Dto","isScalar":false,"associationName":"ManequinManufacturerDto_Manequins","invForeignKeyNames":["manufacturerId"]}]},{"shortName":"ScenarioResourceDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"ScenarioResources","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"scenarioId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"name","dataType":"String","validators":[{"name":"string"},{"name":"required"},{"name":"maxLength","maxLength":50}]},{"name":"resourceFilename","dataType":"String","validators":[{"name":"string"},{"name":"required"},{"name":"maxLength","maxLength":256}]}],"navigationProperties":[{"name":"scenario","entityTypeName":"ScenarioDto:#SM.Dto","isScalar":true,"associationName":"ScenarioResourceDto_Scenario","foreignKeyNames":["scenarioId"]}]},{"shortName":"CourseTeachingResourceDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"CourseTeachingResources","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"courseSlotId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]},{"name":"name","dataType":"String","validators":[{"name":"string"},{"name":"required"},{"name":"maxLength","maxLength":50}]},{"name":"resourceFilename","dataType":"String","validators":[{"name":"string"},{"name":"required"},{"name":"maxLength","maxLength":256}]}],"navigationProperties":[{"name":"courseSlot","entityTypeName":"CourseSlotDto:#SM.Dto","isScalar":true,"associationName":"CourseSlotDto_DefaultResources","foreignKeyNames":["courseSlotId"]}]},{"shortName":"RoomDto","namespace":"SM.Dto","autoGeneratedKeyType":"None","defaultResourceName":"RoomDtoes","dataProperties":[{"name":"id","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","isPartOfKey":true,"validators":[{"name":"required"},{"name":"guid"}]},{"name":"shortDescription","dataType":"String","maxLength":32,"validators":[{"maxLength":32,"name":"maxLength"}]},{"name":"fullDescription","dataType":"String","maxLength":64,"validators":[{"maxLength":64,"name":"maxLength"}]},{"name":"directions","dataType":"String","maxLength":256,"validators":[{"maxLength":256,"name":"maxLength"}]},{"name":"departmentId","dataType":"Guid","isNullable":false,"defaultValue":"00000000-0000-0000-0000-000000000000","validators":[{"name":"required"},{"name":"guid"}]}],"navigationProperties":[{"name":"courses","entityTypeName":"CourseDto:#SM.Dto","isScalar":false,"associationName":"RoomDto_Courses","invForeignKeyNames":["roomId"]},{"name":"department","entityTypeName":"DepartmentDto:#SM.Dto","isScalar":true,"associationName":"DepartmentDto_Rooms","foreignKeyNames":["departmentId"]}]}],"resourceEntityTypeMap":{"Countries":"CountryDto:#SM.Dto","CountryLocales":"CountryLocaleCodeDto:#SM.Dto","Institutions":"InstitutionDto:#SM.Dto","Departments":"DepartmentDto:#SM.Dto","CourseParticipants":"CourseParticipantDto:#SM.Dto","Courses":"CourseDto:#SM.Dto","CourseSlotPresenters":"CourseSlotPresenterDto:#SM.Dto","CourseEvents":"CourseSlotDto:#SM.Dto","CourseTypes":"CourseTypeDto:#SM.Dto","CourseScenarios":"ScenarioSlotDto:#SM.Dto","SenarioRoles":"ScenarioRoleDescriptionDto:#SM.Dto","ScenarioFacultyRoles":"ScenarioFacultyRoleDto:#SM.Dto","Participants":"ParticipantDto:#SM.Dto","ProfessionalRoles":"ProfessionalRoleDto:#SM.Dto","Scenarios":"ScenarioDto:#SM.Dto","Manequins":"ManequinDto:#SM.Dto","ManequinManufacturers":"ManequinManufacturerDto:#SM.Dto","ScenarioResources":"ScenarioResourceDto:#SM.Dto","CourseTeachingResources":"CourseTeachingResourceDto:#SM.Dto","RoomDtoes":"RoomDto:#SM.Dto"}});
	}
	function getBreezeValidators(){
		return {"CountryLocaleCodeDto":["country"],"DepartmentDto":["institution"],"CourseParticipantDto":["department","course","participant","professionalRole"],"CourseDto":["department","courseType","room"],"ParticipantDto":["department","professionalRole"],"RoomDto":["department"],"ScenarioDto":["department","courseType"],"CourseSlotPresenterDto":["course","courseSlot","presenter"],"ScenarioFacultyRoleDto":["course","role","facultyMember","scenario"],"CourseTeachingResourceDto":["courseSlot"],"ScenarioResourceDto":["scenario"],"ManequinDto":["manufacturer"]};
	}
})();

