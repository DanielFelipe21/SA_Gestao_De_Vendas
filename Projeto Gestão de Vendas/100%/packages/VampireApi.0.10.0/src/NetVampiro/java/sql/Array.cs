/*
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at 
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.sql
{
    /**
     * A Java representation of the SQL {@code ARRAY} type.
     */
    public interface Array
    {

        /**
         * Retrieves the contents of the SQL {@code ARRAY} value as a Java array
         * object.
         * 
         * @return A Java array containing the elements of this Array
         * @throws SQLException
         *             if there is a database error.
         */
        Object getArray();// throws SQLException;

        /**
         * Returns part of the SQL {@code ARRAY} associated with this array,
         * starting at a particular {@code index} and comprising up to {@code count}
         * successive elements of the SQL array.
         * 
         * @param index
         *            the start position in the array where the values are
         *            retrieved.
         * @param count
         *            the number of elements to retrieve.
         * @return A Java array containing the desired set of elements from this Array
         * @throws SQLException
         *             if there is a database error.
         */
        Object getArray(long index, int count);// throws SQLException;

        /**
         * Returns part of the SQL {@code ARRAY} associated with this array,
         * starting at a particular {@code index} and comprising up to {@code count}
         * successive elements of the SQL array.
         * 
         * @param index
         *            the start position in the array where the values are
         *            retrieved.
         * @param count
         *            the number of elements to retrieve.
         * @param map
         *            the map defining the correspondence between SQL type names 
         *            and Java types.
         * @return A Java array containing the desired set of elements from this Array
         * @throws SQLException
         *             if there is a database error.
         */
        Object getArray(long index, int count, java.util.Map<String, java.lang.Class> map)
               ;// throws SQLException;

        /**
         * Returns the data from the underlying SQL {@code ARRAY} as a Java array.
         * 
         * @param map
         *            the map defining the correspondence between SQL type names 
         *            and Java types.
         * @return A Java array containing the elements of this array
         * @throws SQLException
         *             if there is a database error.
         */
        Object getArray(java.util.Map<String, java.lang.Class> map);// throws SQLException;

        /**
         * Returns the JDBC type of the entries in this array's underlying 
         * SQL array.
         * 
         * @return An integer constant from the {@code java.sql.Types} class
         * @throws SQLException
         *             if there is a database error.
         */
        int getBaseType();// throws SQLException;

        /**
         * Returns the SQL type name of the entries in this array's underlying 
         * SQL array.
         * 
         * @return The database specific name or a fully-qualified SQL type name.
         * @throws SQLException
         *              if there is a database error.
         */
        String getBaseTypeName();// throws SQLException;

        /**
         * Returns a ResultSet object which holds the entries of the SQL {@code
         * ARRAY} associated with this array.
         * 
         * @return the elements of the array as a {@code ResultSet}.
         * @throws SQLException
         *             if there is a database error.
         */
        ResultSet getResultSet();// throws SQLException;

        /**
         * Returns a {@code ResultSet} object that holds the entries of a subarray,
         * beginning at a particular index and comprising up to {@code count} 
         * successive entries.
         * 
         * @param index
         *            the start position in the array where the values are
         *            retrieved.
         * @param count
         *            the number of elements to retrieve.
         * @return the elements of the array as a {@code ResultSet}.
         * @throws SQLException
         *             if there is a database error.
         */
        ResultSet getResultSet(long index, int count);// throws SQLException;

        /**
         * Returns a {@code ResultSet} object that holds the entries of a subarray,
         * beginning at a particular index and comprising up to {@code count} 
         * successive entries.
         * 
         * @param index
         *            the start position in the array where the values are
         *            retrieved.
         * @param count
         *            the number of elements to retrieve.
         * @param map
         *            the map defining the correspondence between SQL type names 
         *            and Java types.
         * @return the {@code ResultSet} the array's custom type values. if a
         *         database error has occurred.
         * @throws SQLException
         *             if there is a database error.
         */
        ResultSet getResultSet(long index, int count,
                java.util.Map<String, java.lang.Class> map);// throws SQLException;

        /**
         * Returns a {@code ResultSet} object which holds the entries of the SQL
         * {@code ARRAY} associated with this array.
         * 
         * @param map
         *            the map defining the correspondence between SQL type names 
         *            and Java types.
         * @return the array as a {@code ResultSet}.
         * @throws SQLException
         *             if there is a database error.
         */
        ResultSet getResultSet(java.util.Map<String, java.lang.Class> map)
               ;// throws SQLException;

        /**
         * TODO Javadoc
         * 
         * @throws SQLException
         */
        void free();// throws SQLException;
    }

}