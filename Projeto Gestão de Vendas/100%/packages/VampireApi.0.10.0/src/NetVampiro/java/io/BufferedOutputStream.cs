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

namespace biz.ritter.javapi.io
{
    /**
     * Wraps an existing {@link OutputStream} and <em>buffers</em> the output.
     * Expensive interaction with the underlying input stream is minimized, since
     * most (smaller) requests can be satisfied by accessing the buffer alone. The
     * drawback is that some extra space is required to hold the buffer and that
     * copying takes place when flushing that buffer, but this is usually outweighed
     * by the performance benefits.
     * 
     * <p/>A typical application pattern for the class looks like this:<p/>
     *
     * <pre>
     * BufferedOutputStream buf = new BufferedOutputStream(new FileOutputStream(&quot;file.java&quot;));
     * </pre>
     *
     * @see BufferedInputStream
     */
    public class BufferedOutputStream : FilterOutputStream {
        /**
         * The buffer containing the bytes to be written to the target stream.
         */
        protected byte[] buf;

        /**
         * The total number of bytes inside the byte array {@code buf}.
         */
        protected int count;

        /**
         * Constructs a new {@code BufferedOutputStream} on the {@link OutputStream}
         * {@code out}. The buffer size is set to the default value of 8 KB.
         * 
         * @param out
         *            the {@code OutputStream} for which write operations are
         *            buffered.
         */
        public BufferedOutputStream(OutputStream outJ):base(outJ) {
            buf = new byte[8192];
        }

        /**
         * Constructs a new {@code BufferedOutputStream} on the {@link OutputStream}
         * {@code out}. The buffer size is set to {@code size}.
         * 
         * @param out
         *            the output stream for which write operations are buffered.
         * @param size
         *            the size of the buffer in bytes.
         * @throws IllegalArgumentException
         *             if {@code size &lt;= 0}.
         */
        public BufferedOutputStream(OutputStream outJ, int size) :base (outJ){
            if (size <= 0) {
                // luni.A3=size must be > 0
                throw new java.lang.IllegalArgumentException("size must be > 0"); //$NON-NLS-1$
            }
            buf = new byte[size];
        }

        /**
         * Flushes this stream to ensure all pending data is written out to the
         * target stream. In addition, the target stream is flushed.
         * 
         * @throws IOException
         *             if an error occurs attempting to flush this stream.
         */
        public override void flush() { //throws IOException {
            lock (this) {
                flushInternal();
                outJ.flush();
            }
        }

        /**
         * Writes {@code count} bytes from the byte array {@code buffer} starting at
         * {@code offset} to this stream. If there is room in the buffer to hold the
         * bytes, they are copied in. If not, the buffered bytes plus the bytes in
         * {@code buffer} are written to the target stream, the target is flushed,
         * and the buffer is cleared.
         * 
         * @param buffer
         *            the buffer to be written.
         * @param offset
         *            the start position in {@code buffer} from where to get bytes.
         * @param length
         *            the number of bytes from {@code buffer} to write to this
         *            stream.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code length &lt; 0}, or if
         *             {@code offset + length} is greater than the size of
         *             {@code buffer}.
         * @throws IOException
         *             if an error occurs attempting to write to this stream.
         * @throws NullPointerException
         *             if {@code buffer} is {@code null}.
         * @throws ArrayIndexOutOfBoundsException
         *             If offset or count is outside of bounds.
         */
        public override void write(byte[] buffer, int offset, int length)
        {//throws IOException {
            byte[] internalBuffer = buf;

            if (internalBuffer != null && length >= internalBuffer.Length) {
                flushInternal();
                outJ.write(buffer, offset, length);
                return;
            }

            if (buffer == null) {
                // luni.11=buffer is null
                throw new java.lang.NullPointerException("buffer is null"); //$NON-NLS-1$
            }
        
            if (offset < 0 || offset > buffer.Length - length) {
                // luni.12=Offset out of bounds \: {0}
                throw new java.lang.ArrayIndexOutOfBoundsException("Offset out of bounds : "+ offset); //$NON-NLS-1$
        
            }
            if (length < 0) {
                // luni.18=Length out of bounds \: {0}
                throw new java.lang.ArrayIndexOutOfBoundsException("Length out of bounds : "+ length); //$NON-NLS-1$
            }

            if (internalBuffer == null) {
                throw new IOException("Stream is closed"); //$NON-NLS-1$
            }

            // flush the internal buffer first if we have not enough space left
            if (length >= (internalBuffer.Length - count)) {
                flushInternal();
            }

            // the length is always less than (internalBuffer.length - count) here so arraycopy is safe
            java.lang.SystemJ.arraycopy(buffer, offset, internalBuffer, count, length);
            count += length;
        }

        public override void close() {//throws IOException {
            lock (this) {
                if (buf == null) {
                    return;
                }
        
                try {
                    base.close();
                } finally {
                    buf = null;
                }
            }
        }

        /**
         * Writes one byte to this stream. Only the low order byte of the integer
         * {@code oneByte} is written. If there is room in the buffer, the byte is
         * copied into the buffer and the count incremented. Otherwise, the buffer
         * plus {@code oneByte} are written to the target stream, the target is
         * flushed, and the buffer is reset.
         * 
         * @param oneByte
         *            the byte to be written.
         * @throws IOException
         *             if an error occurs attempting to write to this stream.
         */
        
        public override void write(int oneByte) {//throws IOException {
            lock (this)
            {
                byte[] internalBuffer = buf;
                if (internalBuffer == null)
                {
                    throw new IOException("Stream is closed"); //$NON-NLS-1$
                }

                if (count == internalBuffer.Length)
                {
                    outJ.write(internalBuffer, 0, count);
                    count = 0;
                }
                internalBuffer[count++] = (byte)oneByte;
            }
        }

        /**
         * Flushes only internal buffer.
         */
        private void flushInternal() {//throws IOException {
            if (count > 0) {
                outJ.write(buf, 0, count);
                count = 0;
            }
        }
    }
}
