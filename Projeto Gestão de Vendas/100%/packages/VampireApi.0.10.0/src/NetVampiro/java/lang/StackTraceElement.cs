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
// some implementation parts comes from Apache Harmony project
using System;
using System.Text;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang
{

    /**
     * A representation of a single stack frame. Arrays of {@code StackTraceElement}
     * are stored in {@link Throwable} objects to represent the whole state of the
     * call stack at the time a {@code Throwable} gets thrown.
     *
     * @see Throwable#getStackTrace()
     * @since 1.4
     */
[Serializable]
    public sealed class StackTraceElement : java.io.Serializable {

        private static readonly long serialVersionUID = 6992337162326171013L;

        internal String declaringClass;

        internal String methodName;

        internal String fileName;

        internal int lineNumber;

        /**
         * Constructs a new {@code StackTraceElement} for a specified execution
         * point.
         *
         * @param cls
         *            the fully qualified name of the class where execution is at.
         * @param method
         *            the name of the method where execution is at.
         * @param file
         *            The name of the file where execution is at or {@code null}.
         * @param line
         *            the line of the file where execution is at, a negative number
         *            if unknown or {@code -2} if the execution is in a native
         *            method.
         * @throws NullPointerException
         *             if {@code cls} or {@code method} is {@code null}.
         * @since 1.5
         */
        public StackTraceElement(String cls, String method, String file, int line) :base(){
            if (cls == null || method == null) {
                throw new java.lang.NullPointerException();
            }
            declaringClass = cls;
            methodName = method;
            fileName = file;
            lineNumber = line;
        }

        /**
         * <p>
         * Private, nullary constructor for VM use only.
         * </p>
         */
        private StackTraceElement() : base(){
        }

        /**
         * Compares this instance with the specified object and indicates if they
         * are equal. In order to be equal, the following conditions must be
         * fulfilled:
         * <ul>
         * <li>{@code obj} must be a stack trace element,</li>
         * <li>the method names of this stack trace element and of {@code obj} must
         * not be {@code null},</li>
         * <li>the class, method and file names as well as the line number of this
         * stack trace element and of {@code obj} must be equal.</li>
         * </ul>
         *
         * @param obj
         *            the object to compare this instance with.
         * @return {@code true} if the specified object is equal to this
         *         {@code StackTraceElement}; {@code false} otherwise.
         * @see #hashCode
         */
        public override bool Equals(Object obj) {
            if (!(obj is StackTraceElement)) {
                return false;
            }
            StackTraceElement castObj = (StackTraceElement) obj;

            /*
             * Unknown methods are never equal to anything (not strictly to spec,
             * but spec does not allow null method/class names)
             */
            if ((methodName == null) || (castObj.methodName == null)) {
                return false;
            }

            if (!getMethodName().equals(castObj.getMethodName())) {
                return false;
            }
            if (!getClassName().equals(castObj.getClassName())) {
                return false;
            }
            String localFileName = getFileName();
            if (localFileName == null) {
                if (castObj.getFileName() != null) {
                    return false;
                }
            } else {
                if (!localFileName.equals(castObj.getFileName())) {
                    return false;
                }
            }
            if (getLineNumber() != castObj.getLineNumber()) {
                return false;
            }

            return true;
        }

        /**
         * Returns the fully qualified name of the class belonging to this
         * {@code StackTraceElement}.
         * 
         * @return the fully qualified type name of the class
         */
        public String getClassName() {
            return (declaringClass == null) ? "<unknown class>" : declaringClass;
        }

        /**
         * Returns the name of the Java source file containing class belonging to
         * this {@code StackTraceElement}.
         * 
         * @return the name of the file, or {@code null} if this information is not
         *         available.
         */
        public String getFileName() {
            return fileName;
        }

        /**
         * Returns the line number in the source for the class belonging to this
         * {@code StackTraceElement}.
         * 
         * @return the line number, or a negative number if this information is not
         *         available.
         */
        public int getLineNumber() {
            return lineNumber;
        }

        /**
         * Returns the name of the method belonging to this {@code
         * StackTraceElement}.
         * 
         * @return the name of the method, or "&lt;unknown method&gt;" if this information
         *         is not available.
         */
        public String getMethodName() {
            return (methodName == null) ? "<unknown method>" : methodName;
        }

        public override int GetHashCode() {
            /*
             * Either both methodName and declaringClass are null, or neither are
             * null.
             */
            if (methodName == null) {
                // all unknown methods hash the same
                return 0;
            }
            // declaringClass never null if methodName is non-null
            return methodName.GetHashCode() ^ declaringClass.GetHashCode();
        }

        /**
         * Indicates if the method name returned by {@link #getMethodName()} is
         * implemented as a native method.
         * 
         * @return {@code true} if the method in which this stack trace element is
         *         executing is a native method; {@code false} otherwise.
         */
        public bool isNativeMethod() {
            return lineNumber == -2;
        }

        public override String ToString() {
            StringBuilder buf = new StringBuilder(80);

            buf.append(getClassName());
            buf.append('.');
            buf.append(getMethodName());

            if (isNativeMethod()) {
                buf.append("(Native Method)");
            } else {
                String fName = getFileName();

                if (fName == null) {
                    buf.append("(Unknown Source)");
                } else {
                    int lineNum = getLineNumber();

                    buf.append('(');
                    buf.append(fName);
                    if (lineNum >= 0) {
                        buf.append(':');
                        buf.append(lineNum);
                    }
                    buf.append(')');
                }
            }
            return buf.toString();
        }
    }
}
