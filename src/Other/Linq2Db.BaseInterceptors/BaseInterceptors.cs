using LinqToDB.CodeModel;
using LinqToDB.DataModel;
using LinqToDB.Scaffold;
using LinqToDB.Schema;

namespace Linq2Db.BaseInterceptors
{
    public class BaseInterceptors : ScaffoldInterceptors
    {
#pragma warning disable IDE0052 // Удалить непрочитанные закрытые члены
        private ScaffoldOptions Options { get; }
#pragma warning restore IDE0052 // Удалить непрочитанные закрытые члены

        // don't add other constructors (they will be ignored), as interceptors class instantiated using default constructor or constructor with ScaffoldOptions parameter
        public BaseInterceptors(ScaffoldOptions options)
        {
            Options = options;
            // Options could be used in two ways:
            // - it contains settings, passed with command line
            // - it could be used to configure options programmatically (it should be done here in constructor before scaffolder will start using them)
        }

        #region Schema Interceptors
        /// <summary>
        /// Using this method user could add, remove or modify table schema information.
        /// </summary>
        /// <param name="tables">Table schema, provided by schema provider.</param>
        /// <returns>Table schema with interceptor logic applied.</returns>
        public override IEnumerable<Table> GetTables(IEnumerable<Table> tables)
        {
            return tables;
        }

        /// <summary>
        /// Using this method user could add, remove or modify view schema information.
        /// </summary>
        /// <param name="views">View schema, provided by schema provider.</param>
        /// <returns>View schema with interceptor logic applied.</returns>
        public override IEnumerable<View> GetViews(IEnumerable<View> views)
        {
            return views;
        }

        /// <summary>
        /// Using this method user could add, remove or modify foreign key constrains schema information.
        /// </summary>
        /// <param name="keys">Foreign key schema, provided by schema provider.</param>
        /// <returns>Foreign key schema with interceptor logic applied.</returns>
        public override IEnumerable<ForeignKey> GetForeignKeys(IEnumerable<ForeignKey> keys)
        {
            return keys;
        }

        /// <summary>
        /// Using this method user could add, remove or modify stored procedure schema information.
        /// </summary>
        /// <param name="procedures">Stored procedure schema, provided by schema provider.</param>
        /// <returns>Stored procedure schema with interceptor logic applied.</returns>
        public override IEnumerable<StoredProcedure> GetProcedures(IEnumerable<StoredProcedure> procedures)
        {
            return procedures;
        }

        /// <summary>
        /// Using this method user could add, remove or modify table function schema information.
        /// </summary>
        /// <param name="functions">Table function schema, provided by schema provider.</param>
        /// <returns>Table function schema with interceptor logic applied.</returns>
        public override IEnumerable<TableFunction> GetTableFunctions(IEnumerable<TableFunction> functions)
        {
            return functions;
        }

        /// <summary>
        /// Using this method user could add, remove or modify scalar function schema information.
        /// </summary>
        /// <param name="functions">Scalar function schema, provided by schema provider.</param>
        /// <returns>Scalar function schema with interceptor logic applied.</returns>
        public override IEnumerable<ScalarFunction> GetScalarFunctions(IEnumerable<ScalarFunction> functions)
        {
            return functions;
        }

        /// <summary>
        /// Using this method user could add, remove or modify aggregate function schema information.
        /// </summary>
        /// <param name="functions">Aggregate function schema, provided by schema provider.</param>
        /// <returns>Aggregate function schema with interceptor logic applied.</returns>
        public override IEnumerable<AggregateFunction> GetAggregateFunctions(IEnumerable<AggregateFunction> functions)
        {
            return functions;
        }
        #endregion

        #region Type mapping Interceptors
        /// <summary>
        /// Using this method user could specify which .NET type and <see cref="DataType"/> enum value to use with specific database type.
        /// Method called only once per database type.
        /// <see cref="TypeMapping.CLRType"/> shouldn't be a nullable type, as nullability applied to it later automatically based on owning object (e.g. column or procedure parameter) nullability.
        /// </summary>
        /// <param name="databaseType">Database type specification.</param>
        /// <param name="typeParser">Type parser to create value for <see cref="TypeMapping.CLRType"/> property from <see cref="Type"/> instance or type name string.</param>
        /// <param name="defaultMapping">Default type mapping for specified <paramref name="databaseType"/>.</param>
        /// <returns>Type mapping information for specified <paramref name="databaseType"/>.</returns>
        public override TypeMapping? GetTypeMapping(DatabaseType databaseType, ITypeParser typeParser, TypeMapping? defaultMapping)
        {
            return defaultMapping;
        }
        #endregion

        #region Data Model/Code Generation
        /// <summary>
        /// Using this method user could modify entity code generation options:
        /// <list type="bullet">
        /// <item>modify associated table/view metadata descriptor: <see cref="EntityModel.Metadata"/></item>
        /// <item>modify entity class code generation options including custom attributes: <see cref="EntityModel.Class"/></item>
        /// <item>modify/add/remove data context table access property: <see cref="EntityModel.ContextProperty"/></item>
        /// <item>edit list of generated Find/FindAsync/FindQuery extensions: <see cref="EntityModel.FindExtensions"/></item>
        /// <item>modify column list: <see cref="EntityModel.Columns"/> including column metadata and property code generation options.</item>
        /// </list>
        /// </summary>
        /// <param name="typeParser">Type parser service to create type tokens.</param>
        /// <param name="entityModel">Entity model descriptor.</param>
        public override void PreprocessEntity(ITypeParser typeParser, EntityModel entityModel)
        {
            //var column = entityModel.Columns.FirstOrDefault(x => !string.IsNullOrEmpty(x.Metadata.Name) && x.Metadata.Name.ToLower() == "id" && x.Property.Type == typeParser.Parse(typeof(Guid)));

            //var hasId = entityModel.Columns.Any(x => !string.IsNullOrEmpty(x.Metadata.Name) && x.Metadata.Name.ToLower() == "id" && x.Property.Type == typeParser.Parse(typeof(Guid)));
            //var hasDates = entityModel.Columns.Any(x => !string.IsNullOrEmpty(x.Metadata.Name) && x.Metadata.Name.ToLower() == "createat" && x.Property.Type == typeParser.Parse(typeof(DateTime)));
            //if (hasId && hasDates) entityModel.Class.BaseType = typeParser.Parse(typeof(BaseEntity));
            //else if (hasId) entityModel.Class.BaseType = typeParser.Parse(typeof(BaseEntityWithoutDates));
            //else if (hasDates) entityModel.Class.BaseType = typeParser.Parse(typeof(BaseEntityDates));
            //else entityModel.Class.BaseType = typeParser.Parse(typeof(BaseEmptyEntity));

        }

        /// <summary>
        /// Using this method user could modify stored procedure code generation options:
        /// <list type="bullet">
        /// <item>Return parameter descriptor: <see cref="StoredProcedureModel.Return"/></item>
        /// <item>Return tables (data sets) descriptor: <see cref="StoredProcedureModel.Results"/></item>
        /// <item>Error, returned by data set schema load procedure: <see cref="TableFunctionModelBase.Error"/></item>
        /// <item>Metadata (procedure name): <see cref="TableFunctionModelBase.Name"/></item>
        /// <item>Method code-generation options: <see cref="FunctionModelBase.Method"/></item>
        /// <item>Parameters: <see cref="FunctionModelBase.Parameters"/></item>
        /// </list>
        /// </summary>
        /// <param name="typeParser">Type parser service to create type tokens.</param>
        /// <param name="procedureModel">Stored procedure model descriptor.</param>
        public override void PreprocessStoredProcedure(ITypeParser typeParser, StoredProcedureModel procedureModel)
        {
        }

        /// <summary>
        /// Using this method user could modify table function code generation options:
        /// <list type="bullet">
        /// <item>Function metadata: <see cref="TableFunctionModel.Metadata"/></item>
        /// <item>Return table descriptor: <see cref="TableFunctionModel.Result"/></item>
        /// <item>Error, returned by data set schema load procedure: <see cref="TableFunctionModelBase.Error"/></item>
        /// <item>Metadata (function name): <see cref="TableFunctionModelBase.Name"/></item>
        /// <item>Method code-generation options: <see cref="FunctionModelBase.Method"/></item>
        /// <item>Parameters: <see cref="FunctionModelBase.Parameters"/></item>
        /// </list>
        /// </summary>
        /// <param name="typeParser">Type parser service to create type tokens.</param>
        /// <param name="functionModel">Function model descriptor.</param>
        public override void PreprocessTableFunction(ITypeParser typeParser, TableFunctionModel functionModel)
        {
        }

        /// <summary>
        /// Using this method user could modify scalar function code generation options:
        /// <list type="bullet">
        /// <item>Return scalar value or tuple descriptor: <see cref="ScalarFunctionModel.Return"/> or <see cref="ScalarFunctionModel.ReturnTuple"/></item>
        /// <item>Function metadata: <see cref="ScalarFunctionModelBase.Metadata"/></item>
        /// <item>Method code-generation options: <see cref="FunctionModelBase.Method"/></item>
        /// <item>Parameters: <see cref="FunctionModelBase.Parameters"/></item>
        /// </list>
        /// </summary>
        /// <param name="typeParser">Type parser service to create type tokens.</param>
        /// <param name="functionModel">Function model descriptor.</param>
        public override void PreprocessScalarFunction(ITypeParser typeParser, ScalarFunctionModel functionModel)
        {
        }

        /// <summary>
        /// Using this method user could modify aggregate function code generation options:
        /// <list type="bullet">
        /// <item>Return type: <see cref="AggregateFunctionModel.ReturnType"/></item>
        /// <item>Function metadata: <see cref="ScalarFunctionModelBase.Metadata"/></item>
        /// <item>Method code-generation options: <see cref="FunctionModelBase.Method"/></item>
        /// <item>Parameters: <see cref="FunctionModelBase.Parameters"/></item>
        /// </list>
        /// </summary>
        /// <param name="typeParser">Type parser service to create type tokens.</param>
        /// <param name="functionModel">Function model descriptor.</param>
        public override void PreprocessAggregateFunction(ITypeParser typeParser, AggregateFunctionModel functionModel)
        {
        }

        /// <summary>
        /// Using this method user could modify association code generation options:
        /// <list type="bullet">
        /// <item>Metadata for both sides of association: <see cref="AssociationModel.SourceMetadata"/> and <see cref="AssociationModel.TargetMetadata"/></item>
        /// <item>Configure association properties in entity classes: <see cref="AssociationModel.Property"/> and <see cref="AssociationModel.BackreferenceProperty"/></item>
        /// <item>Configure association extension methods: <see cref="AssociationModel.Extension"/> and <see cref="AssociationModel.BackreferenceExtension"/></item>
        /// <item>Association cardinality: <see cref="AssociationModel.ManyToOne"/></item>
        /// </list>
        /// Also it is possible to modify set of columns, used by association, but it is probably not very useful.
        /// </summary>
        /// <param name="typeParser">Type parser service to create type tokens.</param>
        /// <param name="associationModel">Association model descriptor.</param>
        public override void PreprocessAssociation(ITypeParser typeParser, AssociationModel associationModel)
        {
            if (associationModel.BackreferenceProperty != null && associationModel.Source.Metadata.Name != null)
                associationModel.BackreferenceProperty.Name = associationModel.Source.Metadata.Name.Value.Name;
        }
        #endregion
    }
}
