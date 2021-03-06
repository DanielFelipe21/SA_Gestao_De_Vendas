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

namespace biz.ritter.javapi.nio
{

    /**
     * A buffer is a list of elements of a specific primitive type.
     * <p>
     * A buffer can be described by the following properties:
     * <ul>
     * <li>Capacity: the number of elements a buffer can hold. Capacity may not be
     * negative and never changes.</li>
     * <li>Position: a cursor of this buffer. Elements are read or written at the
     * position if you do not specify an index explicitly. Position may not be
     * negative and not greater than the limit.</li>
     * <li>Limit: controls the scope of accessible elements. You can only read or
     * write elements from index zero to <code>limit - 1</code>. Accessing
     * elements out of the scope will cause an exception. Limit may not be negative
     * and not greater than capacity.</li>
     * <li>Mark: used to remember the current position, so that you can reset the
     * position later. Mark may not be negative and no greater than position.</li>
     * <li>A buffer can be read-only or read-write. Trying to modify the elements
     * of a read-only buffer will cause a <code>ReadOnlyBufferException</code>,
     * while changing the position, limit and mark of a read-only buffer is OK.</li>
     * <li>A buffer can be direct or indirect. A direct buffer will try its best to
     * take advantage of native memory APIs and it may not stay in the Java heap,
     * thus it is not affected by garbage collection.</li>
     * </ul>
     * <p>
     * Buffers are not thread-safe. If concurrent access to a buffer instance is
     * required, then the callers are responsible to take care of the
     * synchronization issues.
     */
    /// <remarks>Class is ported from Apache Harmony project.</remarks>
    public abstract class Buffer
    {

        /**
         * <code>UNSET_MARK</code> means the mark has not been set.
         */
        public readonly static int UNSET_MARK = -1;

        /// <summary>
        /// The capacity of this buffer, which never change. 
        /// </summary>
        protected internal readonly int capacityJ;

        /**
         * <code>limit - 1</code> is the last element that can be read or written.
         * Limit must be no less than zero and no greater than <code>capacity</code>.
         */
        protected internal int limitJ;

        /**
         * Mark is where position will be set when <code>reset()</code> is called.
         * Mark is not set by default. Mark is always no less than zero and no
         * greater than <code>position</code>.
         */
        protected internal int markJ = UNSET_MARK;

        /// <summary>
        /// The current position of this buffer. Position is always no less than zero
        /// and no greater than <code>limit</code>.
        /// </summary>
        protected internal int positionJ = 0;

        /**
         * Construct a buffer with the specified capacity.
         * 
         * @param capacity
         *            The capacity of this buffer
         */
        protected internal Buffer(int capacity) : base () {
            if (capacity < 0) {
                throw new java.lang.IllegalArgumentException();
            }
            this.capacityJ = capacity;
            this.limitJ = capacity;
        }

    
        /**
         * Answers the array that backs this buffer.
         * 
         * It wants to allow array-backed buffers to be passed to native code more
         * efficiently. Subclasses provide more concrete return values for this
         * method.
         * 
         * Modifications to this buffer's content will cause the returned array's
         * content to be modified, and vice versa.
         * 
         * Invoking the <code>hasArray</code> method before invoking this method
         * can guarantee it to be called safely.
         * 
         * @return The array that backs this buffer
         * 
         * @throws ReadOnlyBufferException -
         *             If this buffer is backed by an array but is read-only
         *         UnsupportedOperationException - If this buffer is not backed
         *             by an accessible array
         * @since 1.6
         * 
         */
        public abstract Object array();
    
        /**
         * Returns the offset within this buffer's backing array of the first
         * element of the buffer (optional operation).
         * 
         * If this buffer is backed by an array then buffer position p corresponds
         * to array index p + arrayOffset().
         * 
         * Invoke the hasArray method before invoking this method in order to ensure
         * that this buffer has an accessible backing array.
         * 
         * @return The offset within this buffer's array of the first element of the
         *         buffer
         * 
         * @throws ReadOnlyBufferException -
         *             If this buffer is backed by an array but is read-only
         *         UnsupportedOperationException - If this buffer is not backed
         *             by an accessible array
         * 
         * @since 1.6
         */
        public abstract int arrayOffset();

        /**
         * Returns the capacity of this buffer.
         * 
         * @return the number of elements that are contained in this buffer.
         */
        public int capacity() {
            return this.capacityJ;
        }

        /**
         * Clears this buffer.
         * <p>
         * While the content of this buffer is not changed, the following internal
         * changes take place: the current position is reset back to the start of
         * the buffer, the value of the buffer limit is made equal to the capacity
         * and mark is cleared.
         *
         * @return this buffer.
         */
        public Buffer clear() {
            positionJ = 0;
            markJ = UNSET_MARK;
            limitJ = capacityJ;
            return this;
        }

        /**
         * Flips this buffer.
         * <p>
         * The limit is set to the current position, then the position is set to
         * zero, and the mark is cleared.
         * <p>
         * The content of this buffer is not changed.
         *
         * @return this buffer.
         */
        public Buffer flip() {
            limitJ = positionJ;
            positionJ = 0;
            markJ = UNSET_MARK;
            return this;
        }


    
        /**
         * Answers if this buffer is backed by an available array.
         * 
         * If it returns true then the <code>array</code> and
         * <code>arrayOffset</code> methods can be called safely.
         * 
         * @return true if and only if this buffer is backed by an array and is
         *         not read-only.
         *         
         * @since 1.6
         */
        public abstract bool hasArray();

        /**
         * Indicates if there are elements remaining in this buffer, that is if
         * {@code position < limit}.
         * 
         * @return {@code true} if there are elements remaining in this buffer,
         *         {@code false} otherwise.
         */
        public bool hasRemaining() {
            return positionJ < limitJ;
        }
    
        /**
         * Answers if this buffer is direct.
         * 
         * @return true if and only if this buffer is direct
         * 
         * @since 1.6
         */
        public abstract bool isDirect();

        /**
         * Indicates whether this buffer is read-only.
         * 
         * @return {@code true} if this buffer is read-only, {@code false}
         *         otherwise.
         */
        public abstract bool isReadOnly();

        /**
         * Returns the limit of this buffer.
         * 
         * @return the limit of this buffer.
         */
        public int limit() {
            return limitJ;
        }

        /**
         * Sets the limit of this buffer.
         * <p>
         * If the current position in the buffer is in excess of
         * <code>newLimit</code> then, on returning from this call, it will have
         * been adjusted to be equivalent to <code>newLimit</code>. If the mark
         * is set and is greater than the new limit, then it is cleared.
         *
         * @param newLimit
         *            the new limit, must not be negative and not greater than
         *            capacity.
         * @return this buffer.
         * @exception IllegalArgumentException
         *                if <code>newLimit</code> is invalid.
         */
        public Buffer limit(int newLimit) {
            if (newLimit < 0 || newLimit > capacityJ) {
                throw new java.lang.IllegalArgumentException();
            }

            limitJ = newLimit;
            if (positionJ > newLimit) {
                positionJ = newLimit;
            }
            if ((markJ != UNSET_MARK) && (markJ > newLimit)) {
                markJ = UNSET_MARK;
            }
            return this;
        }

        /**
         * Marks the current position, so that the position may return to this point
         * later by calling <code>reset()</code>.
         * 
         * @return this buffer.
         */
        public Buffer mark() {
            markJ = positionJ;
            return this;
        }

        /**
         * Returns the position of this buffer.
         * 
         * @return the value of this buffer's current position.
         */
        public int position() {
            return positionJ;
        }

        /**
         * Sets the position of this buffer.
         * <p>
         * If the mark is set and it is greater than the new position, then it is
         * cleared.
         *
         * @param newPosition
         *            the new position, must be not negative and not greater than
         *            limit.
         * @return this buffer.
         * @exception IllegalArgumentException
         *                if <code>newPosition</code> is invalid.
         */
        public Buffer position(int newPosition) {
            if (newPosition < 0 || newPosition > limitJ) {
                throw new java.lang.IllegalArgumentException();
            }

            positionJ = newPosition;
            if ((markJ != UNSET_MARK) && (markJ > positionJ)) {
                markJ = UNSET_MARK;
            }
            return this;
        }

        /// <summary>
        /// Returns the number of remaining elements in this buffer, that is
        /// <c>limit - position</c>.
        /// </summary>
        /// <returns>the number of remaining elements in this buffer.</returns>
        public int remaining() {
            return limitJ - positionJ;
        }

        /**
         * Resets the position of this buffer to the <code>mark</code>.
         * 
         * @return this buffer.
         * @exception InvalidMarkException
         *                if the mark is not set.
         */
        public Buffer reset() {
            if (markJ == UNSET_MARK) {
                throw new InvalidMarkException();
            }
            positionJ = markJ;
            return this;
        }

        /**
         * Rewinds this buffer.
         * <p>
         * The position is set to zero, and the mark is cleared. The content of this
         * buffer is not changed.
         *
         * @return this buffer.
         */
        public Buffer rewind() {
            positionJ = 0;
            markJ = UNSET_MARK;
            return this;
        }
    }
}
