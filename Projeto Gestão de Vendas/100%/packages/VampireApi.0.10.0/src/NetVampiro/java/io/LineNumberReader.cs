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
     * Wraps an existing {@link Reader} and counts the line terminators encountered
     * while reading the data. The line number starts at 0 and is incremented any
     * time {@code '\r'}, {@code '\n'} or {@code "\r\n"} is read. The class has an
     * internal buffer for its data. The size of the buffer defaults to 8 KB.
     */
    public class LineNumberReader : BufferedReader
    {

        private int lineNumber;

        private int markedLineNumber = -1;

        private bool lastWasCR;

        private bool markedLastWasCR;

        /**
         * Constructs a new LineNumberReader on the Reader {@code in}. The internal
         * buffer gets the default size (8 KB).
         * 
         * @param in
         *            the Reader that is buffered.
         */
        public LineNumberReader(Reader inJ)
            : base(inJ)
        {
        }

        /**
         * Constructs a new LineNumberReader on the Reader {@code in}. The size of
         * the internal buffer is specified by the parameter {@code size}.
         * 
         * @param in
         *            the Reader that is buffered.
         * @param size
         *            the size of the buffer to allocate.
         * @throws IllegalArgumentException
         *             if {@code size &lt;= 0}.
         */
        public LineNumberReader(Reader inJ, int size)
            : base(inJ, size)
        {
        }

        /**
         * Returns the current line number for this reader. Numbering starts at 0.
         * 
         * @return the current line number.
         */
        public int getLineNumber()
        {
            lock (this.lockJ)
            {
                return lineNumber;
            }
        }

        /**
         * Sets a mark position in this reader. The parameter {@code readlimit}
         * indicates how many characters can be read before the mark is invalidated.
         * Sending {@code reset()} will reposition this reader back to the marked
         * position, provided that {@code readlimit} has not been surpassed. The
         * line number associated with this marked position is also stored so that
         * it can be restored when {@code reset()} is called.
         * 
         * @param readlimit
         *            the number of characters that can be read from this stream
         *            before the mark is invalidated.
         * @throws IOException
         *             if an error occurs while setting the mark in this reader.
         * @see #markSupported()
         * @see #reset()
         */

        public override void mark(int readlimit)
        {//throws IOException {
            lock (lockJ)
            {
                base.mark(readlimit);
                markedLineNumber = lineNumber;
                markedLastWasCR = lastWasCR;
            }
        }

        /**
         * Reads a single character from the source reader and returns it as an
         * integer with the two higher-order bytes set to 0. Returns -1 if the end
         * of the source reader has been reached.
         * <p>
         * The line number count is incremented if a line terminator is encountered.
         * Recognized line terminator sequences are {@code '\r'}, {@code '\n'} and
         * {@code "\r\n"}. Line terminator sequences are always translated into
         * {@code '\n'}.
         *</p>
         * @return the character read or -1 if the end of the source reader has been
         *         reached.
         * @throws IOException
         *             if the reader is closed or another IOException occurs.
         */
        public override int read()
        {//throws IOException {
            lock (lockJ)
            {
                int ch = base.read();
                if (ch == '\n' && lastWasCR)
                {
                    ch = base.read();
                }
                lastWasCR = false;
                switch (ch)
                {
                    case '\r':
                        ch = '\n';
                        lastWasCR = true;
                        // fall through
                        break;
                    case '\n':
                        #region copy of case '\r':
                        ch = '\n';
                        lastWasCR = true;
                        #endregion
                        lineNumber++;
                        break;
                }
                return ch;
            }
        }

        /**
         * Reads at most {@code count} characters from the source reader and stores
         * them in the character array {@code buffer} starting at {@code offset}.
         * Returns the number of characters actually read or -1 if no characters
         * have been read and the end of this reader has been reached.
         * <p>
         * The line number count is incremented if a line terminator is encountered.
         * Recognized line terminator sequences are {@code '\r'}, {@code '\n'} and
         * {@code "\r\n"}.
         *</p>
         * @param buffer
         *            the array in which to store the characters read.
         * @param offset
         *            the initial position in {@code buffer} to store the characters
         *            read from this reader.
         * @param count
         *            the maximum number of characters to store in {@code buffer}.
         * @return the number of characters actually read or -1 if the end of the
         *         source reader has been reached while reading.
         * @throws IOException
         *             if this reader is closed or another IOException occurs.
         */

        public override int read(char[] buffer, int offset, int count)
        {//throws IOException {
            lock (lockJ)
            {
                int read = base.read(buffer, offset, count);
                if (read == -1)
                {
                    return -1;
                }
                for (int i = 0; i < read; i++)
                {
                    char ch = buffer[offset + i];
                    if (ch == '\r')
                    {
                        lineNumber++;
                        lastWasCR = true;
                    }
                    else if (ch == '\n')
                    {
                        if (!lastWasCR)
                        {
                            lineNumber++;
                        }
                        lastWasCR = false;
                    }
                    else
                    {
                        lastWasCR = false;
                    }
                }
                return read;
            }
        }

        /**
         * Returns the next line of text available from this reader. A line is
         * represented by 0 or more characters followed by {@code '\r'},
         * {@code '\n'}, {@code "\r\n"} or the end of the stream. The returned
         * string does not include the newline sequence.
         * 
         * @return the contents of the line or {@code null} if no characters have
         *         been read before the end of the stream has been reached.
         * @throws IOException
         *             if this reader is closed or another IOException occurs.
         */

        public override String readLine()
        {//throws IOException {
            lock (lockJ)
            {
                if (lastWasCR)
                {
                    chompNewline();
                    lastWasCR = false;
                }
                String result = base.readLine();
                if (result != null)
                {
                    lineNumber++;
                }
                return result;
            }
        }

        /**
         * Resets this reader to the last marked location. It also resets the line
         * count to what is was when this reader was marked. This implementation
         * resets the source reader.
         * 
         * @throws IOException
         *             if this reader is already closed, no mark has been set or the
         *             mark is no longer valid because more than {@code readlimit}
         *             bytes have been read since setting the mark.
         * @see #mark(int)
         * @see #markSupported()
         */

        public override void reset()
        {// throws IOException {
            lock (lockJ)
            {
                base.reset();
                lineNumber = markedLineNumber;
                lastWasCR = markedLastWasCR;
            }
        }

        /**
         * Sets the line number of this reader to the specified {@code lineNumber}.
         * Note that this may have side effects on the line number associated with
         * the last marked position.
         * 
         * @param lineNumber
         *            the new line number value.
         * @see #mark(int)
         * @see #reset()
         */
        public void setLineNumber(int lineNumber)
        {
            lock (lockJ)
            {
                this.lineNumber = lineNumber;
            }
        }

        /**
         * Skips {@code count} number of characters in this reader. Subsequent
         * {@code read()}'s will not return these characters unless {@code reset()}
         * is used. This implementation skips {@code count} number of characters in
         * the source reader and increments the line number count whenever line
         * terminator sequences are skipped.
         * 
         * @param count
         *            the number of characters to skip.
         * @return the number of characters actually skipped.
         * @throws IllegalArgumentException
         *             if {@code count &lt; 0}.
         * @throws IOException
         *             if this reader is closed or another IOException occurs.
         * @see #mark(int)
         * @see #read()
         * @see #reset()
         */

        public override long skip(long count)
        {// throws IOException {
            if (count < 0)
            {
                throw new java.lang.IllegalArgumentException();
            }
            lock (lockJ)
            {
                for (int i = 0; i < count; i++)
                {
                    if (read() == -1)
                    {
                        return i;
                    }
                }
                return count;
            }
        }
    }
}