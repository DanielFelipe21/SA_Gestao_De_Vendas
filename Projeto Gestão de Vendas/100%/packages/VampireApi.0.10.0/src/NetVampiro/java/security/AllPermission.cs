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

namespace biz.ritter.javapi.security
{
    /**
     * {@code AllPermission} represents the permission to perform any operation.
     * Since its {@link #implies(Permission)} method always returns {@code true},
     * granting this permission is equivalent to disabling security.
     */
    [Serializable]
    public sealed class AllPermission : Permission
    {

        /**
         * @serial
         */
        private static readonly long serialVersionUID = -2916474571451318075L;

        // Permission name
        private const String ALL_PERMISSIONS = "<all permissions>"; //$NON-NLS-1$

        // Actions name
        private const String ALL_ACTIONS = "<all actions>"; //$NON-NLS-1$

        /**
         * Constructs a new instance of {@code AllPermission}. The two argument
         * version is provided for class {@code Policy} so that it has a consistent
         * call pattern across all permissions. The name and action list are both
         * ignored.
         *
         * @param name
         *            ignored.
         * @param actions
         *            ignored.
         */
        public AllPermission(String name, String actions) :
            base(ALL_PERMISSIONS)
        {
        }

        /**
         * Constructs a new instance of {@code AllPermission}.
         */
        public AllPermission() :
            base(ALL_PERMISSIONS)
        {
        }

        /**
         * Compares the specified object with this {@code AllPermission} for
         * equality and returns {@code true} if the specified object is equal,
         * {@code false} otherwise. To be equal, the given object needs to be an
         * instance of {@code AllPermission}.
         *
         * @param obj
         *            object to be compared for equality with this {@code
         *            AllPermission}.
         * @return {@code true} if the specified object is equal to this {@code
         *         AllPermission}, otherwise {@code false}.
         * @see #hashCode
         */
        public override bool Equals(Object obj)
        {
            return (obj is AllPermission);
        }

        /**
         * Returns the hash code value for this {@code AllPermission}. Returns the
         * same hash code for {@code AllPermission}s that are equal to each other as
         * required by the general contract of {@link Object#hashCode}.
         *
         * @return the hash code value for this {@code AllPermission}.
         * @see Object#equals(Object)
         * @see AllPermission#equals(Object)
         */
        public override int GetHashCode()
        {
            return 1;
        }

        /**
         * Returns the actions associated with this {@code AllPermission}. Since
         * {@code AllPermission} objects allow all actions, this method returns
         * always the string "&lt;all actions&gt;".
         *
         * @return the actions associated with this {@code AllPermission}.
         */
        public override String getActions()
        {
            return ALL_ACTIONS;
        }

        /**
         * Indicates whether the given permission is implied by this permission.
         * {@code AllPermission} objects imply all other permissions.
         *
         * @return always {@code true}.
         * @param permission
         *            the permission to check.
         */
        public override bool implies(Permission permission)
        {
            return true;
        }

        /**
         * Returns a new {@code PermissionCollection} for holding permissions of
         * this class.
         *
         * @return a new {@code PermissionCollection}.
         */
        public override PermissionCollection newPermissionCollection()
        {
            return new AllPermissionCollection();
        }
    }
}