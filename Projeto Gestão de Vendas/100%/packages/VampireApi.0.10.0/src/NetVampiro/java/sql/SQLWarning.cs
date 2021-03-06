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
     * An exception class that holds information about Database access warnings.
     */
    [Serializable]
    public class SQLWarning : SQLException, java.io.Serializable
    {

        private const long serialVersionUID = 3917336774604784856L;

        /**
         * Creates an {@code SQLWarning} object. The reason string is set to {@code
         * null}, the {@code SQLState} string is set to {@code null} and the error
         * code is set to 0.
         */
        public SQLWarning() :
            base()
        {
        }

        /**
         * Creates an {@code SQLWarning} object. The reason string is set to the
         * given reason string, the {@code SQLState} string is set to {@code null}
         * and the error code is set to 0.
         * 
         * @param theReason
         *            the reason why this warning is issued.
         */
        public SQLWarning(String theReason) :
            base(theReason)
        {
        }

        /**
         * Creates an {@code SQLWarning} object. The reason string is set to the
         * given reason string, the {@code SQLState} string is set to the given
         * {@code SQLState} string and the error code is set to 0.
         * 
         * @param theReason
         *            the reason why this warning is issued.
         * @param theSQLState
         *            the string to use as the {@code SQLState} string.
         */
        public SQLWarning(String theReason, String theSQLState) :
            base(theReason, theSQLState)
        {
        }

        /**
         * Creates an {@code SQLWarning} object. The reason string is set to the
         * given reason string, the {@code SQLState} string is set to the given
         * {@code SQLState} string and the error code is set to the given error code
         * value.
         * 
         * @param theReason
         *            the reason why this warning is issued.
         * @param theSQLState
         *            the X/Open standard specifc error code.
         * @param theErrorCode
         *            a vendor specific error code.
         */
        public SQLWarning(String theReason, String theSQLState, int theErrorCode) :
            base(theReason, theSQLState, theErrorCode)
        {
        }

        /**
         * Gets the next {@code SQLWarning} chained to this {@code SQLWarning} object.
         * 
         * @return the {@code SQLWarning} chained to this {@code SQLWarning}.
         *         {@code null} if no {@code SQLWarning} is chained to this {@code
         *         SQLWarning}.
         */
        public SQLWarning getNextWarning()
        {
            SQLException next = base.getNextException();
            if (next == null)
            {
                return null;
            }
            if (next is SQLWarning)
            {
                return (SQLWarning)next;
            }
            throw new java.lang.Error("SQLWarning chain holds value that is not a SQLWarning"); //$NON-NLS-1$
        }

        /**
         * Creates an SQLWarning object. The Reason string is set to null, the
         * SQLState string is set to null and the Error Code is set to 0, cause is
         * set to cause.
         * 
         * @since 1.6
         */
        public SQLWarning(java.lang.Throwable cause) :
            base(cause)
        {
        }

        /**
         * Creates an SQLWarning object. The Reason string is set to reason, the
         * SQLState string is set to null and the Error Code is set to 0, cause is
         * set to the given cause
         * 
         * @since 1.6
         */
        public SQLWarning(String reason, java.lang.Throwable cause) :
            base(reason, cause)
        {
        }

        /**
         * Creates an SQLWarning object. The Reason string is set to reason, the
         * SQLState string is set to given SQLState and the Error Code is set to 0,
         * cause is set to the given cause
         * 
         * @since 1.6
         */
        public SQLWarning(String reason, String SQLState, java.lang.Throwable cause) :
            base(reason, SQLState, cause)
        {
        }

        /**
         * Creates an SQLWarning object. The Reason string is set to reason, the
         * SQLState string is set to given SQLState and the Error Code is set to
         * vendorCode, cause is set to the given cause
         * 
         * @since 1.6
         */
        public SQLWarning(String reason, String SQLState, int vendorCode,
                java.lang.Throwable cause) :
            base(reason, SQLState, vendorCode, cause)
        {
        }

        /**
         * Chains a supplied {@code SQLWarning} to this {@code SQLWarning}.
         * 
         * @param w
         *            the {@code SQLWarning} linked to this {@code SQLWarning}.
         */
        public void setNextWarning(SQLWarning w)
        {
            base.setNextException(w);
        }
    }

}