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

namespace biz.ritter.javapi.util.zip
{
    public class InflaterInputStream : java.io.FilterInputStream {

        /**
         * The inflater used for this stream.
         */
        protected Inflater inf;

        /**
         * The input buffer used for decompression.
         */
        protected byte[] buf;

        internal const int BUF_SIZE = 512;

        /**
         * This is the most basic constructor. You only need to pass the {@code
         * InputStream} from which the compressed data is to be read from. Default
         * settings for the {@code Inflater} and internal buffer are be used. In
         * particular the Inflater expects a ZLIB header from the input stream.
         *
         * @param is
         *            the {@code InputStream} to read data from.
         */
        public InflaterInputStream(java.io.InputStream isJ) :this(isJ, new Inflater(), BUF_SIZE){
        }

        /**
         * This constructor lets you pass a specifically initialized Inflater,
         * for example one that expects no ZLIB header.
         *
         * @param is
         *            the {@code InputStream} to read data from.
         * @param inf
         *            the specific {@code Inflater} for uncompressing data.
         */
        public InflaterInputStream(java.io.InputStream isJ, Inflater inf) :this(isJ, inf, BUF_SIZE){
        }

        /**
         * This constructor lets you specify both the {@code Inflater} as well as
         * the internal buffer size to be used.
         *
         * @param is
         *            the {@code InputStream} to read data from.
         * @param inf
         *            the specific {@code Inflater} for uncompressing data.
         * @param bsize
         *            the size to be used for the internal buffer.
         */
        public InflaterInputStream(java.io.InputStream isJ, Inflater inf, int bsize) : base (isJ){
            if (isJ == null || inf == null) {
                throw new java.lang.NullPointerException();
            }
            if (bsize <= 0) {
                throw new java.lang.IllegalArgumentException();
            }
            this.inf = inf;
            buf = new byte[bsize];
        }


        /**
         * The length of the buffer.
         */
        protected int len;

        protected internal bool closed;

        /**
         * True if this stream's last byte has been returned to the user. This
         * could be because the underlying stream has been exhausted, or if errors
         * were encountered while inflating that stream.
         */
        protected internal bool eof;

        /**
         * Reads a single byte of decompressed data.
         *
         * @return the byte read.
         * @throws IOException
         *             if an error occurs reading the byte.
         */
    
        public override int read() {//throws IOException {
            byte[] b = new byte[1];
            if (read(b, 0, 1) == -1) {
                return -1;
            }
            return b[0] & 0xff;
        }

        /**
         * Reads up to {@code nbytes} of decompressed data and stores it in
         * {@code buffer} starting at {@code off}.
         *
         * @param buffer
         *            the buffer to write data to.
         * @param off
         *            offset in buffer to start writing.
         * @param nbytes
         *            number of bytes to read.
         * @return Number of uncompressed bytes read
         * @throws IOException
         *             if an IOException occurs.
         */
    
        public override int read(byte[] buffer, int off, int nbytes) {//throws IOException {
            /* archive.1E=Stream is closed */
            if (closed) {
                throw new java.io.IOException("Stream is closed"); //$NON-NLS-1$
            }

            if (null == buffer) {
                throw new java.lang.NullPointerException();
            }

            if (off < 0 || nbytes < 0 || off + nbytes > buffer.Length) {
                throw new java.lang.IndexOutOfBoundsException();
            }

            if (nbytes == 0) {
                return 0;
            }

            if (eof) {
                return -1;
            }

            // avoid int overflow, check null buffer
            if (off > buffer.Length || nbytes < 0 || off < 0
                    || buffer.Length - off < nbytes) {
                throw new java.lang.ArrayIndexOutOfBoundsException();
            }

            do {
                if (inf.needsInput()) {
                    fill();
                }
                // Invariant: if reading returns -1 or throws, eof must be true.
                // It may also be true if the next read() should return -1.
                try {
                    int result = inf.inflate(buffer, off, nbytes);
                    eof = inf.finished();
                    if (result > 0) {
                        return result;
                    } else if (eof) {
                        return -1;
                    } else if (inf.needsDictionary()) {
                        eof = true;
                        return -1;
                    } else if (len == -1) {
                        eof = true;
                        throw new java.io.EOFException();
                        // If result == 0, fill() and try again
                    }
                } catch (DataFormatException e) {
                    eof = true;
                    if (len == -1) {
                        throw new java.io.EOFException();
                    }
                    throw (java.io.IOException) (new java.io.IOException().initCause(e));
                }
            } while (true);
        }

        /**
         * Fills the input buffer with data to be decompressed.
         *
         * @throws IOException
         *             if an {@code IOException} occurs.
         */
        protected void fill(){// throws IOException {
            if (closed) {
                throw new java.io.IOException("Stream is closed"); //$NON-NLS-1$
            }
            if ((len = inJ.read(buf)) > 0) {
                inf.setInput(buf, 0, len);
            }
        }

        /**
         * Skips up to n bytes of uncompressed data.
         *
         * @param nbytes
         *            the number of bytes to skip.
         * @return the number of uncompressed bytes skipped.
         * @throws IOException
         *             if an error occurs skipping.
         */
       
        public override long skip(long nbytes) {//throws IOException {
            if (nbytes >= 0) {
                if (buf == null) {
                    buf = new byte[(int)java.lang.Math.min(nbytes, BUF_SIZE)];
                }
                long count = 0, rem = 0;
                while (count < nbytes) {
                    int x = read(buf, 0,
                            (rem = nbytes - count) > buf.Length ? buf.Length
                                    : (int) rem);
                    if (x == -1) {
                        return count;
                    }
                    count += x;
                }
                return count;
            }
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * Returns 0 when this stream has exhausted its input; and 1 otherwise.
         * A result of 1 does not guarantee that further bytes can be returned,
         * with or without blocking.
         *
         * <p>Although consistent with the RI, this behavior is inconsistent with
         * {@link InputStream#available()}, and violates the <a
         * href="http://en.wikipedia.org/wiki/Liskov_substitution_principle">Liskov
         * Substitution Principle</a>. This method should not be used.
         *
         * @return 0 if no further bytes are available. Otherwise returns 1,
         *         which suggests (but does not guarantee) that additional bytes are
         *         available.
         * @throws IOException
         *             If an error occurs.
         */
    
        public override int available() {//throws IOException {
            if (closed) {
                // archive.1E=Stream is closed
                throw new java.io.IOException("Stream is closed"); //$NON-NLS-1$
            }
            if (eof) {
                return 0;
            }
            return 1;
        }

        /**
         * Closes the input stream.
         *
         * @throws IOException
         *             If an error occurs closing the input stream.
         */
        public override void close() {//throws IOException {
            if (!closed) {
                inf.end();
                closed = true;
                eof = true;
                base.close();
            }
        }

        /**
         * Marks the current position in the stream. This implementation overrides
         * the super type implementation to do nothing at all.
         *
         * @param readlimit
         *            of no use.
         */
        public override void mark(int readlimit) {
            // do nothing
        }

        /**
         * Reset the position of the stream to the last marked position. This
         * implementation overrides the supertype implementation and always throws
         * an {@link IOException IOException} when called.
         *
         * @throws IOException
         *             if the method is called
         */
        public override void reset() {//throws IOException {
            throw new java.io.IOException();
        }

        /**
         * Returns whether the receiver implements {@code mark} semantics. This type
         * does not support {@code mark()}, so always responds {@code false}.
         *
         * @return false, always
         */
    
        public override bool markSupported() {
            return false;
        }

    }
}
