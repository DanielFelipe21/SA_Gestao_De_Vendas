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
     * This class is an actual usage of the wrapper pattern for JDBC classes.
     * Developers can get the delegate instance when the instance may be a proxy
     * class.
     * 
     * @since 1.6
     */
    public interface Wrapper
    {

        /**
         * Answers an object that implements the given interface. If the caller is
         * not a wrapper, a SQLException will be thrown.
         * 
         * @param iface -
         *            the class that defines the interface
         * @return - an object that implements the interface
         * @throws SQLException -
         *             if there is no object implementing the specific interface
         */
        T unwrap<T>(java.lang.Class iface);// throws SQLException;

        /**
         * If the caller is a wrapper of the class or implements the given
         * interface, the methods return false and vice versa.
         * 
         * @param iface -
         *            the class that defines the interface
         * @return - true if the instance implements the interface
         * @throws SQLException -
         *             when an error occurs when judges the object
         */
        bool isWrapperFor(java.lang.Class iface);// throws SQLException;
    }
}