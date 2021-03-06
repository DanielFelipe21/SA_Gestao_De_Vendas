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
using System.Text;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.io
{

    /**
     * Wraps an existing {@link Reader} and <em>buffers</em> the input. Expensive
     * interaction with the underlying reader is minimized, since most (smaller)
     * requests can be satisfied by accessing the buffer alone. The drawback is that
     * some extra space is required to hold the buffer and that copying takes place
     * when filling that buffer, but this is usually outweighed by the performance
     * benefits.
     * 
     * <p/>A typical application pattern for the class looks like this:<p/>
     *
     * <pre>
     * BufferedReader buf = new BufferedReader(new FileReader(&quot;file.java&quot;));
     * </pre>
     * 
     * @see BufferedWriter
     * @since 1.1
     */
    public class BufferedReader : Reader {

        private const String MESSAGE_READER_IS_CLOSED = "reader is closed";
        private Reader inJ;

        /**
         * The characters that can be read and refilled in bulk. We maintain three
         * indices into this buffer:<pre>
         *     { X X X X X X X X X X X X - - }
         *           ^     ^             ^
         *           |     |             |
         *         mark   pos           end</pre>
         * Pos points to the next readable character. End is one greater than the
         * last readable character. When {@code pos == end}, the buffer is empty and
         * must be {@link #fillBuf() filled} before characters can be read.
         *
         * <p>Mark is the value pos will be set to on calls to {@link #reset}. Its
         * value is in the range {@code [0...pos]}. If the mark is {@code -1}, the
         * buffer cannot be reset.</p>
         *
         * <p>MarkLimit limits the distance between the mark and the pos. When this
         * limit is exceeded, {@link #reset} is permitted (but not required) to
         * throw an exception. For shorter distances, {@link #reset} shall not throw
         * (unless the reader is closed).</p>
         */
        private char[] buf;

        private int pos;

        private int end;

        private int markJ = -1;

        private int markLimit = -1;

        /**
         * Constructs a new BufferedReader on the Reader {@code in}. The
         * buffer gets the default size (8 KB).
         * 
         * @param in
         *            the Reader that is buffered.
         */
        public BufferedReader(Reader inJ) : base (inJ) {
            this.inJ = inJ;
            buf = new char[8192];
        }

        /**
         * Constructs a new BufferedReader on the Reader {@code in}. The buffer
         * size is specified by the parameter {@code size}.
         * 
         * @param in
         *            the Reader that is buffered.
         * @param size
         *            the size of the buffer to allocate.
         * @throws IllegalArgumentException
         *             if {@code size &lt;= 0}.
         */
        public BufferedReader(Reader inJ, int size) : base (inJ) {
            if (size <= 0) {
                throw new java.lang.IllegalArgumentException("size <= 0"); //$NON-NLS-1$
            }
            this.inJ = inJ;
            buf = new char[size];
        }

        /**
         * Closes this reader. This implementation closes the buffered source reader
         * and releases the buffer. Nothing is done if this reader has already been
         * closed.
         * 
         * @throws IOException
         *             if an error occurs while closing this reader.
         */
        public override void close() //throws IOException 
        {
            lock (lockJ) {
                if (!isClosed()) {
                    inJ.close();
                    buf = null;
                }
            }
        }

        /**
         * Populates the buffer with data. It is an error to call this method when
         * the buffer still contains data; ie. if {@code pos &lt; end}.
         *
         * @return the number of bytes read into the buffer, or -1 if the end of the
         *      source stream has been reached.
         */
        private int fillBuf() //throws IOException 
        {
            // assert(pos == end);

            if (markJ == -1 || (pos - markJ >= markLimit)) {
                /* mark isn't set or has exceeded its limit. use the whole buffer */
                int result = inJ.read(buf, 0, buf.Length);
                if (result > 0) {
                    markJ = -1;
                    pos = 0;
                    end = result;
                }
                return result;
            }

            if (markJ == 0 && markLimit > buf.Length) {
                /* the only way to make room when mark=0 is by growing the buffer */
                int newLength = buf.Length * 2;
                if (newLength > markLimit) {
                    newLength = markLimit;
                }
                char[] newbuf = new char[newLength];
                java.lang.SystemJ.arraycopy(buf, 0, newbuf, 0, buf.Length);
                buf = newbuf;
            } else if (markJ > 0) {
                /* make room by shifting the buffered data to left mark positions */
                java.lang.SystemJ.arraycopy(buf, markJ, buf, 0, buf.Length - markJ);
                pos -= markJ;
                end -= markJ;
                markJ = 0;
            }

            /* Set the new position and mark position */
            int count = inJ.read(buf, pos, buf.Length - pos);
            if (count != -1) {
                end += count;
            }
            return count;
        }

        /**
         * Indicates whether or not this reader is closed.
         * 
         * @return {@code true} if this reader is closed, {@code false}
         *         otherwise.
         */
        private bool isClosed() {
            return buf == null;
        }

        /**
         * Sets a mark position in this reader. The parameter {@code markLimit}
         * indicates how many characters can be read before the mark is invalidated.
         * Calling {@code reset()} will reposition the reader back to the marked
         * position if {@code markLimit} has not been surpassed.
         * 
         * @param markLimit
         *            the number of characters that can be read before the mark is
         *            invalidated.
         * @throws IllegalArgumentException
         *             if {@code markLimit &lt; 0}.
         * @throws IOException
         *             if an error occurs while setting a mark in this reader.
         * @see #markSupported()
         * @see #reset()
         */
        public override void mark(int markLimit) //throws IOException 
        {
            if (markLimit < 0) {
                throw new java.lang.IllegalArgumentException();
            }
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                this.markLimit = markLimit;
                markJ = pos;
            }
        }

        /**
         * Indicates whether this reader supports the {@code mark()} and
         * {@code reset()} methods. This implementation returns {@code true}.
         * 
         * @return {@code true} for {@code BufferedReader}.
         * @see #mark(int)
         * @see #reset()
         */
        public override bool markSupported() {
            return true;
        }

        /**
         * Reads a single character from this reader and returns it with the two
         * higher-order bytes set to 0. If possible, BufferedReader returns a
         * character from the buffer. If there are no characters available in the
         * buffer, it fills the buffer and then returns a character. It returns -1
         * if there are no more characters in the source reader.
         *
         * @return the character read or -1 if the end of the source reader has been
         *         reached.
         * @throws IOException
         *             if this reader is closed or some other I/O error occurs.
         */
        public override int read() //throws IOException 
        {
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                /* Are there buffered characters available? */
                if (pos < end || fillBuf() != -1) {
                    return buf[pos++];
                }
                return -1;
            }
        }

        /**
         * Reads at most {@code length} characters from this reader and stores them
         * at {@code offset} in the character array {@code buffer}. Returns the
         * number of characters actually read or -1 if the end of the source reader
         * has been reached. If all the buffered characters have been used, a mark
         * has not been set and the requested number of characters is larger than
         * this readers buffer size, BufferedReader bypasses the buffer and simply
         * places the results directly into {@code buffer}.
         * 
         * @param buffer
         *            the character array to store the characters read.
         * @param offset
         *            the initial position in {@code buffer} to store the bytes read
         *            from this reader.
         * @param length
         *            the maximum number of characters to read, must be
         *            non-negative.
         * @return number of characters read or -1 if the end of the source reader
         *         has been reached.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code length &lt; 0}, or if
         *             {@code offset + length} is greater than the size of
         *             {@code buffer}.
         * @throws IOException
         *             if this reader is closed or some other I/O error occurs.
         */
        public override int read(char[] buffer, int offset, int length) //throws IOException 
        {
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                if (offset < 0 || offset > buffer.Length - length || length < 0) {
                    throw new java.lang.IndexOutOfBoundsException();
                }
                int outstanding = length;
                while (outstanding > 0) {

                    /*
                     * If there are bytes in the buffer, grab those first.
                     */
                    int available = end - pos;
                    if (available > 0) {
                        int count = available >= outstanding ? outstanding : available;
                        java.lang.SystemJ.arraycopy(buf, pos, buffer, offset, count);
                        pos += count;
                        offset += count;
                        outstanding -= count;
                    }

                    /*
                     * Before attempting to read from the underlying stream, make
                     * sure we really, really want to. We won't bother if we're
                     * done, or if we've already got some bytes and reading from the
                     * underlying stream would block.
                     */
                    if (outstanding == 0 || (outstanding < length && !inJ.ready())) {
                        break;
                    }

                    // assert(pos == end);

                    /*
                     * If we're unmarked and the requested size is greater than our
                     * buffer, read the bytes directly into the caller's buffer. We
                     * don't read into smaller buffers because that could result in
                     * a many reads.
                     */
                    if ((markJ == -1 || (pos - markJ >= markLimit))
                            && outstanding >= buf.Length) {
                        int count = inJ.read(buffer, offset, outstanding);
                        if (count > 0) {
                            offset += count;
                            outstanding -= count;
                            markJ = -1;
                        }

                        break; // assume the source stream gave us all that it could
                    }

                    if (fillBuf() == -1) {
                        break; // source is exhausted
                    }
                }

                int count2 = length - outstanding;
                return (count2 > 0 || count2 == length) ? count2 : -1;
            }
        }

        /**
         * Peeks at the next input character, refilling the buffer if necessary. If
         * this character is a newline character ("\n"), it is discarded.
         */
        protected internal void chompNewline() //throws IOException 
        {
            if ((pos != end || fillBuf() != -1)
                    && buf[pos] == '\n') {
                pos++;
            }
        }

        /**
         * Returns the next line of text available from this reader. A line is
         * represented by zero or more characters followed by {@code '\n'},
         * {@code '\r'}, {@code "\r\n"} or the end of the reader. The string does
         * not include the newline sequence.
         * 
         * @return the contents of the line or {@code null} if no characters were
         *         read before the end of the reader has been reached.
         * @throws IOException
         *             if this reader is closed or some other I/O error occurs.
         */
        public virtual String readLine() //throws IOException 
        {
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                /* has the underlying stream been exhausted? */
                if (pos == end && fillBuf() == -1) {
                    return null;
                }
                for (int charPos = pos; charPos < end; charPos++) {
                    char ch = buf[charPos];
                    if (ch > '\r') {
                        continue;
                    }
                    if (ch == '\n') {
                        String res = new String(buf, pos, charPos - pos);
                        pos = charPos + 1;
                        return res;
                    } else if (ch == '\r') {
                        String res = new String(buf, pos, charPos - pos);
                        pos = charPos + 1;
                        if (((pos < end) || (fillBuf() != -1))
                                && (buf[pos] == '\n')) {
                            pos++;
                        }
                        return res;
                    }
                }

                char eol = '\0';
                StringBuilder result = new StringBuilder(80);
                /* Typical Line Length */

                result.Append(buf, pos, end - pos);
                while (true) {
                    pos = end;

                    /* Are there buffered characters available? */
                    if (eol == '\n') {
                        return result.toString();
                    }
                    // attempt to fill buffer
                    if (fillBuf() == -1) {
                        // characters or null.
                        return result.Length > 0 || eol != '\0'
                                ? result.toString()
                                : null;
                    }
                    for (int charPos = pos; charPos < end; charPos++) {
                        char c = buf[charPos];
                        if (eol == '\0') {
                            if ((c == '\n' || c == '\r')) {
                                eol = c;
                            }
                        } else if (eol == '\r' && c == '\n') {
                            if (charPos > pos) {
                                result.Append(buf, pos, charPos - pos - 1);
                            }
                            pos = charPos + 1;
                            return result.toString();
                        } else {
                            if (charPos > pos) {
                                result.Append(buf, pos, charPos - pos - 1);
                            }
                            pos = charPos;
                            return result.toString();
                        }
                    }
                    if (eol == '\0') {
                        result.Append(buf, pos, end - pos);
                    } else {
                        result.Append(buf, pos, end - pos - 1);
                    }
                }
            }

        }

        /**
         * Indicates whether this reader is ready to be read without blocking.
         *
         * @return {@code true} if this reader will not block when {@code read} is
         *         called, {@code false} if unknown or blocking will occur.
         * @throws IOException
         *             if this reader is closed or some other I/O error occurs.
         * @see #read()
         * @see #read(char[], int, int)
         * @see #readLine()
         */
        public override bool ready() //throws IOException 
        {
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                return ((end - pos) > 0) || inJ.ready();
            }
        }

        /**
         * Resets this reader's position to the last {@code mark()} location.
         * Invocations of {@code read()} and {@code skip()} will occur from this new
         * location.
         * 
         * @throws IOException
         *             if this reader is closed or no mark has been set.
         * @see #mark(int)
         * @see #markSupported()
         */
        public override void reset()// throws IOException 
        {
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                if (markJ == -1) {
                    throw new IOException("mark not supported"); //$NON-NLS-1$
                }
                pos = markJ;
            }
        }

        /**
         * Skips {@code amount} characters in this reader. Subsequent
         * {@code read()}s will not return these characters unless {@code reset()}
         * is used. Skipping characters may invalidate a mark if {@code markLimit}
         * is surpassed.
         * 
         * @param amount
         *            the maximum number of characters to skip.
         * @return the number of characters actually skipped.
         * @throws IllegalArgumentException
         *             if {@code amount &lt; 0}.
         * @throws IOException
         *             if this reader is closed or some other I/O error occurs.
         * @see #mark(int)
         * @see #markSupported()
         * @see #reset()
         */
        public override long skip(long amount) //throws IOException 
        {
            if (amount < 0) {
                throw new java.lang.IllegalArgumentException();
            }
            lock (lockJ) {
                if (isClosed()) {
                    throw new IOException(MESSAGE_READER_IS_CLOSED); //$NON-NLS-1$
                }
                if (amount < 1) {
                    return 0;
                }
                if (end - pos >= amount) {
                    pos += (int) amount;
                    return amount;
                }

                long read = end - pos;
                pos = end;
                while (read < amount) {
                    if (fillBuf() == -1) {
                        return read;
                    }
                    if (end - pos >= amount - read) {
                        pos += (int) (amount - read);
                        return amount;
                    }
                    // Couldn't get all the characters, skip what we read
                    read += (end - pos);
                    pos = end;
                }
                return amount;
            }
        }
    }
}
