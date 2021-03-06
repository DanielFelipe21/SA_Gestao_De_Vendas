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
 *  
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang
{
    /**
     * The wrapper for the primitive type {@code boolean}.
     *
     * @since 1.0
     */
    [Serializable]
    public sealed class Boolean : java.io.Serializable, Comparable<Boolean> {

        private static readonly long serialVersionUID = -3665804199014368530L;

        /**
         * The boolean value of the receiver.
         */
        private readonly bool value;

        /**
         * The {@link Class} object that represents the primitive type {@code
         * boolean}.
         */
        public static readonly Type TYPE =  new bool[0].GetType().GetElementType();

        // Note: This can't be set to "boolean.class", since *that* is
        // defined to be "java.lang.Boolean.TYPE";

        /**
         * The {@code Boolean} object that represents the primitive value
         * {@code true}.
         */
        public static readonly Boolean TRUE = new Boolean(true);

        /**
         * The {@code Boolean} object that represents the primitive value
         * {@code false}.
         */
        public static readonly Boolean FALSE = new Boolean(false);

        /**
         * Constructs a new {@code Boolean} with its boolean value specified by
         * {@code string}. If {@code string} is not {@code null} and is equal to
         * "true" using a non-case sensitive comparison, the result will be a
         * Boolean representing the primitive value {@code true}, otherwise it will
         * be a Boolean representing the primitive value {@code false}.
         * 
         * @param string
         *            the string representing a boolean value.
         */
        public Boolean(String s) :this(parseBoolean(s)){
        }

        /**
         * Constructs a new {@code Boolean} with the specified primitive boolean
         * value.
         * 
         * @param value
         *            the primitive boolean value, {@code true} or {@code false}.
         */
        public Boolean(bool value) {
            this.value = value;
        }

        /**
         * Gets the primitive value of this boolean, either {@code true} or
         * {@code false}.
         * 
         * @return this object's primitive value, {@code true} or {@code false}.
         */
        public bool booleanValue() {
            return value;
        }

        /**
         * Compares this instance with the specified object and indicates if they
         * are equal. In order to be equal, {@code o} must be an instance of
         * {@code Boolean} and have the same boolean value as this object.
         * 
         * @param o
         *            the object to compare this boolean with.
         * @return {@code true} if the specified object is equal to this
         *         {@code Boolean}; {@code false} otherwise.
         */
        public bool equals(Object o) {
            return (o == this)
                    || ((o is Boolean) && (value == ((Boolean) o).value));
        }

        /**
         * Compares this object to the specified boolean object to determine their
         * relative order.
         * 
         * @param that
         *            the boolean object to compare this object to.
         * @return 0 if the value of this boolean and the value of {@code that} are
         *         equal; a positive value if the value of this boolean is
         *         {@code true} and the value of {@code that} is {@code false}; a
         *         negative value if the value if this boolean is {@code false} and
         *         the value of {@code that} is {@code true}.
         * @see java.lang.Comparable
         * @since 1.5
         */
        public int compareTo(Boolean that) {
            if (that == null) {
                throw new java.lang.NullPointerException();
            }

            if (this.value == that.value) {
                return 0;
            }

            return this.value ? 1 : -1;
        }

        /**
         * Returns an integer hash code for this boolean.
         * 
         * @return this boolean's hash code, which is {@code 1231} for {@code true}
         *         values and {@code 1237} for {@code false} values.
         */
        
        public override int GetHashCode() {
            return value ? 1231 : 1237;
        }

        /**
         * Returns a string containing a concise, human-readable description of this
         * boolean.
         * 
         * @return "true" if the value of this boolean is {@code true}, "false"
         *         otherwise.
         */
        
        public override String ToString() {
            return StringJ.valueOf(value);
        }

        /**
         * Returns the {@code boolean} value of the system property identified by
         * {@code string}.
         * 
         * @param string
         *            the name of the requested system property.
         * @return {@code true} if the system property named by {@code string}
         *         exists and it is equal to "true" using case insensitive
         *         comparison, {@code false} otherwise.
         * @see System#getProperty(String)
         */
        public static bool getBoolean(String s) {
            if (s == null || s.length() == 0) {
                return false;
            }
            return (parseBoolean(SystemJ.getProperty(s)));
        }

        /**
         * Parses the specified string as a {@code boolean}.
         * 
         * @param s
         *            the string representation of a boolean value.
         * @return {@code true} if {@code s} is not {@code null} and is equal to
         *         {@code "true"} using case insensitive comparison, {@code false}
         *         otherwise.
         * @since 1.5
         */
        public static bool parseBoolean(String s) {
            return "true".equalsIgnoreCase(s); //$NON-NLS-1$
        }

        /**
         * Converts the specified boolean to its string representation.
         * 
         * @param value
         *            the boolean to convert.
         * @return "true" if {@code value} is {@code true}, "false" otherwise.
         */
        public static String toString(bool value) {
            return StringJ.valueOf(value);
        }

        /**
         * Parses the specified string as a boolean value.
         * 
         * @param string
         *            the string representation of a boolean value.
         * @return {@code Boolean.TRUE} if {@code string} is equal to "true" using
         *         case insensitive comparison, {@code Boolean.FALSE} otherwise.
         * @see #parseBoolean(String)
         */
        public static Boolean valueOf(String s) {
            return parseBoolean(s) ? Boolean.TRUE : Boolean.FALSE;
        }

        /**
         * Returns a {@code Boolean} instance for the specified boolean value.
         * <p>
         * If it is not necessary to get a new {@code Boolean} instance, it is
         * recommended to use this method instead of the constructor, since it
         * returns its static instances, which results in better performance.
         *
         * @param b
         *            the boolean to convert to a {@code Boolean}.
         * @return {@code Boolean.TRUE} if {@code b} is equal to {@code true},
         *         {@code Boolean.FALSE} otherwise.
         */
        public static Boolean valueOf(bool b) {
            return b ? Boolean.TRUE : Boolean.FALSE;
        }
        /// <summary>
        /// Implicit convert java.lang.Boolean to .net bool type
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator bool(java.lang.Boolean b)
        {
            return b.value;
        }
        /// <summary>
        /// Implicit convert java.lang.Boolean to .net bool type
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator java.lang.Boolean(bool b)
        {
            return b ? Boolean.TRUE : Boolean.FALSE;
        }
    }
}
