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

namespace biz.ritter.javapi.lang
{

    /**
     * Represents the permission to execute a runtime-related function. There is no
     * action list associated with a {@code RuntimePermission}; the user either has
     * the permission or he doesn't.
     */
    [Serializable]
    public sealed class RuntimePermission : java.security.BasicPermission
    {

        private static readonly long serialVersionUID = 7399184964622342223L;

        /**
         * Constants for runtime permissions used in this package.
         */
        internal static readonly RuntimePermission permissionToSetSecurityManager = new RuntimePermission(
                "setSecurityManager"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToCreateSecurityManager = new RuntimePermission(
                "createSecurityManager"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToGetProtectionDomain = new RuntimePermission(
                "getProtectionDomain"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToGetClassLoader = new RuntimePermission(
                "getClassLoader"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToCreateClassLoader = new RuntimePermission(
                "createClassLoader"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToModifyThread = new RuntimePermission(
                "modifyThread"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToModifyThreadGroup = new RuntimePermission(
                "modifyThreadGroup"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToExitVM = new RuntimePermission(
                "exitVM"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToReadFileDescriptor = new RuntimePermission(
                "readFileDescriptor"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToWriteFileDescriptor = new RuntimePermission(
                "writeFileDescriptor"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToQueuePrintJob = new RuntimePermission(
                "queuePrintJob"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToSetFactory = new RuntimePermission(
                "setFactory"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToSetIO = new RuntimePermission(
                "setIO"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToStopThread = new RuntimePermission(
                "stopThread"); //$NON-NLS-1$

        internal static readonly RuntimePermission permissionToSetContextClassLoader = new RuntimePermission(
                "setContextClassLoader"); //$NON-NLS-1$

        /**
         * Creates an instance of {@code RuntimePermission} with the specified name.
         * 
         * @param permissionName
         *            the name of the new permission.
         */
        public RuntimePermission(String permissionName) :
            base(permissionName)
        {
        }

        /**
         * Creates an instance of {@code RuntimePermission} with the specified name
         * and action list. The action list is ignored.
         * 
         * @param name
         *            the name of the new permission.
         * @param actions
         *            ignored.
         */
        public RuntimePermission(String name, String actions) :
            base(name, actions)
        {
        }
    }
}