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
     * {@code BasicPermission} is the common base class of all permissions which
     * have a name but no action lists. A {@code BasicPermission} is granted or it
     * is not.
     * <p>
     * Names of a BasicPermission follow the dot separated, hierarchical property
     * naming convention. Asterisk '*' can be used as wildcards. Either by itself,
     * matching anything, or at the end of the name, immediately preceded by a '.'.
     * For example:
     * 
     * <pre>
     * java.io.*  grants all permissions under the java.io permission hierarchy
     * *          grants all permissions
     * </pre>
     * <p>
     * While this class ignores the action list in the
     * {@link #BasicPermission(String, String)} constructor, subclasses may
     * implement actions on top of this class.
     */
    [Serializable]
    public abstract class BasicPermission : Permission, java.io.Serializable
    {

        private static readonly long serialVersionUID = 6279438298436773498L;

        /**
         * Constructs a new instance of {@code BasicPermission} with the specified
         * name.
         *
         * @param name
         *            the name of the permission.
         * @throws NullPointerException if {@code name} is {@code null}.
         * @throws IllegalArgumentException if {@code name.length() == 0}.
         */
        public BasicPermission(String name)
            : base(name)
        {
            checkName(name);
        }

        /**
         * Constructs a new instance of {@code BasicPermission} with the specified
         * name. The {@code action} parameter is ignored.
         *
         * @param name
         *            the name of the permission.
         * @param action
         *            is ignored.
         * @throws NullPointerException
         *             if {@code name} is {@code null}.
         * @throws IllegalArgumentException
         *             if {@code name.length() == 0}.
         */
        public BasicPermission(String name, String action)
            : base(name)
        {
            checkName(name);
        }

        /**
         * Checks name parameter
         */
        private void checkName(String name)
        {
            if (name == null)
            {
                throw new java.lang.NullPointerException("name must not be null"); //$NON-NLS-1$
            }
            if (name.length() == 0)
            {
                throw new java.lang.IllegalArgumentException("name must not be empty"); //$NON-NLS-1$
            }
        }

        /**
         * Compares the specified object with this {@code BasicPermission} for
         * equality. Returns {@code true} if the specified object has the same class
         * and the two {@code Permissions}s have the same name.
         * <p>
         * The {@link #implies(Permission)} method should be used for making access
         * control checks.
         *
         * @param obj
         *            object to be compared for equality with this {@code
         *            BasicPermission}.
         * @return {@code true} if the specified object is equal to this {@code
         *         BasicPermission}, otherwise {@code false}.
         */
        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (obj != null && obj.getClass() == this.getClass())
            {
                return this.getName().equals(((Permission)obj).getName());
            }
            return false;
        }

        /**
         * Returns the hash code value for this {@code BasicPermission}. Returns the
         * same hash code for {@code BasicPermission}s that are equal to each other
         * as required by the general contract of {@link Object#hashCode}.
         *
         * @return the hash code value for this {@code BasicPermission}.
         * @see Object#equals(Object)
         * @see BasicPermission#equals(Object)
         */
        public override int GetHashCode()
        {
            return getName().GetHashCode();
        }

        /**
         * Returns the actions associated with this permission. Since {@code
         * BasicPermission} instances have no actions, an empty string is returned.
         *
         * @return an empty string.
         */
        public override String getActions()
        {
            return ""; //$NON-NLS-1$
        }

        /**
         * Indicates whether the specified permission is implied by this permission.
         *
         * @param permission
         *            the permission to check against this permission.
         * @return {@code true} if the specified permission is implied by this
         *         permission, {@code false} otherwise.
         */
        public override bool implies(Permission permission)
        {
            if (permission != null && permission.getClass() == this.getClass())
            {
                String name = getName();
                String thatName = permission.getName();
                if (this is java.lang.RuntimePermission)
                {
                    if (thatName.equals("exitVM"))
                    {
                        thatName = "exitVM.*";
                    }
                    if (name.equals("exitVM"))
                    {
                        name = "exitVM.*";
                    }
                }
                return nameImplies(name, thatName);
            }
            return false;
        }

        /**
         * Checks if {@code thisName} implies {@code thatName},
         * accordingly to hierarchical property naming convention.
         * It is assumed that names cannot be {@code null} or empty.
         */
        static bool nameImplies(String thisName, String thatName)
        {
            if (thisName == thatName)
            {
                return true;
            }
            int end = thisName.length();
            if (end > thatName.length())
            {
                return false;
            }
            if (thisName.charAt(--end) == '*'
                && (end == 0 || thisName.charAt(end - 1) == '.'))
            {
                //wildcard found
                end--;
            }
            else if (end != (thatName.length() - 1))
            {
                //names are not equal
                return false;
            }
            for (int i = end; i >= 0; i--)
            {
                if (thisName.charAt(i) != thatName.charAt(i))
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * Returns an empty {@link PermissionCollection} for holding permissions.
         * <p>
         * For {@code PermissionCollection} (and subclasses which do not override
         * this method), the collection which is returned does <em>not</em> invoke
         * the {@link #implies(Permission)} method of the permissions which are
         * stored in it when checking if the collection implies a permission.
         * Instead, it assumes that if the type of the permission is correct, and
         * the name of the permission is correct, there is a match.
         *
         * @return an empty {@link PermissionCollection} for holding permissions.
         * @see BasicPermissionCollection
         */

        public PermissionCollection newPermissionCollection()
        {
            return new BasicPermissionCollection();
        }

        /**
         * Checks name after default deserialization.
         */
        private void readObject(java.io.ObjectInputStream inJ)// throws IOException,
        {//ClassNotFoundException {
            inJ.defaultReadObject();
            checkName(this.getName());
        }
    }
}