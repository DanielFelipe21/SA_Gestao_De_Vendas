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

namespace biz.ritter.javapi.util.zip
{
    /**
     * This class provides random read access to a <i>ZIP-archive</i> file.
     * <p/>
     * While {@code ZipInputStream} provides stream based read access to a
     * <i>ZIP-archive</i>, this class implements more efficient (file based) access
     * and makes use of the <i>central directory</i> within a <i>ZIP-archive</i>.
     * <p/>
     * Use {@code ZipOutputStream} if you want to create an archive.
     * <p/>
     * A temporary ZIP file can be marked for automatic deletion upon closing it.
     *
     * @see ZipEntry
     * @see ZipOutputStream
     */
    public class ZipFile  {
        /// <summary>
        /// Constant for ZIP file handling.
        /// </summary>
        public static readonly long LOCSIG = 0x4034b50, EXTSIG = 0x8074b50,
                CENSIG = 0x2014b50, ENDSIG = 0x6054b50;
        /// <summary>
        /// Constant for ZIP file handling.
        /// </summary>
        public static readonly int LOCHDR = 30, EXTHDR = 16, CENHDR = 46, ENDHDR = 22,
                LOCVER = 4, LOCFLG = 6, LOCHOW = 8, LOCTIM = 10, LOCCRC = 14,
                LOCSIZ = 18, LOCLEN = 22, LOCNAM = 26, LOCEXT = 28, EXTCRC = 4,
                EXTSIZ = 8, EXTLEN = 12, CENVEM = 4, CENVER = 6, CENFLG = 8,
                CENHOW = 10, CENTIM = 12, CENCRC = 16, CENSIZ = 20, CENLEN = 24,
                CENNAM = 28, CENEXT = 30, CENCOM = 32, CENDSK = 34, CENATT = 36,
                CENATX = 38, CENOFF = 42, ENDSUB = 8, ENDTOT = 10, ENDSIZ = 12,
                ENDOFF = 16, ENDCOM = 20;
        /**
         * Open ZIP file for read.
         */
        public static readonly int OPEN_READ = 1;

        /**
         * Delete ZIP file when closed.
         */
        public static readonly int OPEN_DELETE = 4;

        private readonly String fileName;

        private java.io.File fileToDeleteOnClose;

        private java.io.RandomAccessFile mRaf;

        private readonly LittleEndianReader ler = new LittleEndianReader();

        private readonly LinkedHashMap<String, ZipEntry> mEntries
                = new LinkedHashMap<String, ZipEntry>();

        /**
         * Constructs a new {@code ZipFile} with the specified file.
         *
         * @param file
         *            the file to read from.
         * @throws ZipException
         *             if a ZIP error occurs.
         * @throws IOException
         *             if an {@code IOException} occurs.
         */
        public ZipFile(java.io.File file):// throws ZipException, IOException {
            this(file, OPEN_READ){
        }

        /**
         * Opens a file as <i>ZIP-archive</i>. "mode" must be {@code OPEN_READ} or
         * {@code OPEN_DELETE} . The latter sets the "delete on exit" flag through a
         * file.
         *
         * @param file
         *            the ZIP file to read.
         * @param mode
         *            the mode of the file open operation.
         * @throws IOException
         *             if an {@code IOException} occurs.
         */
        public ZipFile(java.io.File file, int mode) {//throws IOException {
            fileName = file.getPath();
            if (mode != OPEN_READ && mode != (OPEN_READ | OPEN_DELETE)) {
                throw new java.lang.IllegalArgumentException();
            }

/*            java.lang.SecurityManager security = java.lang.SystemJ.getSecurityManager();
            if (security != null) {
                security.checkRead(fileName);
            }*/
            if ((mode & OPEN_DELETE) != 0) {
/*                if (security != null) {
                    security.checkDelete(fileName);
                }*/
                fileToDeleteOnClose = file; // file.deleteOnExit();
            } else {
                fileToDeleteOnClose = null;
            }

            mRaf = new java.io.RandomAccessFile(fileName, "r");

            readCentralDir();
        }

        /**
         * Opens a ZIP archived file.
         *
         * @param name
         *            the name of the ZIP file.
         * @throws IOException
         *             if an IOException occurs.
         */
        public ZipFile(String name) ://throws IOException {
            this(new java.io.File(name), OPEN_READ){
        }
        /// <summary>
        /// Destruct instance.
        /// </summary>
        ~ZipFile()
        {
            this.finalize();
        }

        /// <summary>
        /// Clean up instance
        /// </summary>
        protected void finalize(){// throws IOException {
            close();
        }

        /**
         * Closes this ZIP file. This method is idempotent.
         *
         * @throws IOException
         *             if an IOException occurs.
         */
        public void close(){// throws IOException {
            java.io.RandomAccessFile raf = mRaf;

            if (raf != null) { // Only close initialized instances
                lock(raf) {
                    mRaf = null;
                    raf.close();
                }
                if (fileToDeleteOnClose != null) {
                    new java.io.File(fileName).delete();
                    /*AccessController.doPrivileged(new PrivilegedAction<Object>() {
                        public Object run() {
                            new File(fileName).delete();
                            return null;
                        }
                    });*/
                    // fileToDeleteOnClose.delete();
                    fileToDeleteOnClose = null;
                }
            }
        }

        internal void checkNotClosed() {
            if (mRaf == null) {
                throw new java.lang.IllegalStateException("Zip File closed.");
            }
        }

        /**
         * Returns an enumeration of the entries. The entries are listed in the
         * order in which they appear in the ZIP archive.
         *
         * @return the enumeration of the entries.
         * @throws IllegalStateException if this ZIP file has been closed.
         */
        public Enumeration<ZipEntry> entries() {
            checkNotClosed();
            Iterator<ZipEntry> iterator = mEntries.values().iterator();
            return new IAC_ZipEntryEnumeration (iterator,this);
        }

        /**
         * Gets the ZIP entry with the specified name from this {@code ZipFile}.
         *
         * @param entryName
         *            the name of the entry in the ZIP file.
         * @return a {@code ZipEntry} or {@code null} if the entry name does not
         *         exist in the ZIP file.
         * @throws IllegalStateException if this ZIP file has been closed.
         */
        public ZipEntry getEntry(String entryName) {
            checkNotClosed();
            if (entryName == null) {
                throw new java.lang.NullPointerException();
            }

            ZipEntry ze = mEntries.get(entryName);
            if (ze == null) {
                ze = mEntries.get(entryName + "/");
            }
            return ze;
        }

        /**
         * Returns an input stream on the data of the specified {@code ZipEntry}.
         *
         * @param entry
         *            the ZipEntry.
         * @return an input stream of the data contained in the {@code ZipEntry}.
         * @throws IOException
         *             if an {@code IOException} occurs.
         * @throws IllegalStateException if this ZIP file has been closed.
         */
        public java.io.InputStream getInputStream(ZipEntry entry){// throws IOException {
            /*
             * Make sure this ZipEntry is in this Zip file.  We run it through
             * the name lookup.
             */
            entry = getEntry(entry.getName());
            if (entry == null) {
                return null;
            }

            /*
             * Create a ZipInputStream at the right part of the file.
             */
            java.io.RandomAccessFile raf = mRaf;
            lock (raf) {
                // We don't know the entry data's start position. All we have is the
                // position of the entry's local header. At position 28 we find the
                // length of the extra data. In some cases this length differs from
                // the one coming in the central header.
                RAFStream rafstrm = new RAFStream(raf,
                        entry.mLocalHeaderRelOffset + 28);
                int localExtraLenOrWhatever = ler.readShortLE(rafstrm);
                // Skip the name and this "extra" data or whatever it is:
                rafstrm.skip(entry.nameLen + localExtraLenOrWhatever);
                rafstrm.mLength = rafstrm.mOffset + entry.compressedSize;
                if (entry.compressionMethod == ZipEntry.DEFLATED) {
                    int bufSize = java.lang.Math.max(1024, (int)java.lang.Math.min(entry.getSize(), 65535L));
                    return new ZipInflaterInputStream(rafstrm, new Inflater(true), bufSize, entry);
                } else {
                    return rafstrm;
                }
            }
        }

        /**
         * Gets the file name of this {@code ZipFile}.
         *
         * @return the file name of this {@code ZipFile}.
         */
        public String getName() {
            return fileName;
        }

        /**
         * Returns the number of {@code ZipEntries} in this {@code ZipFile}.
         *
         * @return the number of entries in this file.
         * @throws IllegalStateException if this ZIP file has been closed.
         */
        public int size() {
            checkNotClosed();
            return mEntries.size();
        }

        /**
         * Find the central directory and read the contents.
         *
         * <p>The central directory can be followed by a variable-length comment
         * field, so we have to scan through it backwards.  The comment is at
         * most 64K, plus we have 18 bytes for the end-of-central-dir stuff
         * itself, plus apparently sometimes people throw random junk on the end
         * just for the fun of it.</p>
         *
         * <p>This is all a little wobbly.  If the wrong value ends up in the EOCD
         * area, we're hosed. This appears to be the way that everybody handles
         * it though, so we're in good company if this fails.</p>
         */
        private void readCentralDir() {//throws IOException {
            /*
             * Scan back, looking for the End Of Central Directory field.  If
             * the archive doesn't have a comment, we'll hit it on the first
             * try.
             *
             * No need to synchronize mRaf here -- we only do this when we
             * first open the Zip file.
             */
            long scanOffset = mRaf.length() - ENDHDR;
            if (scanOffset < 0) {
                throw new ZipException("too short to be Zip");
            }

            long stopOffset = scanOffset - 65536;
            if (stopOffset < 0) {
                stopOffset = 0;
            }

            while (true) {
                mRaf.seek(scanOffset);
                if (ZipEntry.readIntLE(mRaf) == 101010256L) {
                    break;
                }

                scanOffset--;
                if (scanOffset < stopOffset) {
                    throw new ZipException("EOCD not found; not a Zip archive?");
                }
            }

            /*
             * Found it, read the EOCD.
             *
             * For performance we want to use buffered I/O when reading the
             * file.  We wrap a buffered stream around the random-access file
             * object.  If we just read from the RandomAccessFile we'll be
             * doing a read() system call every time.
             */
            RAFStream rafs = new RAFStream(mRaf, mRaf.getFilePointer());
            java.io.BufferedInputStream bin = new java.io.BufferedInputStream(rafs, ENDHDR);

            int diskNumber = ler.readShortLE(bin);
            int diskWithCentralDir = ler.readShortLE(bin);
            int numEntries = ler.readShortLE(bin);
            int totalNumEntries = ler.readShortLE(bin);
            /*centralDirSize =*/ ler.readIntLE(bin);
            long centralDirOffset = ler.readIntLE(bin);
            /*commentLen =*/ ler.readShortLE(bin);

            if (numEntries != totalNumEntries ||
                diskNumber != 0 ||
                diskWithCentralDir != 0) {
                throw new ZipException("spanned archives not supported");
            }

            /*
             * Seek to the first CDE and read all entries.
             * However, when Z_SYNC_FLUSH is used the offset may not point directly
             * to the CDE so skip over until we find it. 
             * At most it will be 6 bytes away (one or two bytes for empty block, 4 bytes for
             * empty block signature).  
             */
            scanOffset = centralDirOffset;
            stopOffset = scanOffset + 6;
        
            while (true) {
                mRaf.seek(scanOffset);
                if (ZipEntry.readIntLE(mRaf) == CENSIG) {
                    break;
                }

                scanOffset++;
                if (scanOffset > stopOffset) {
                    throw new ZipException("Central Directory Entry not found");
                }
            }
        
            // If CDE is found then go and read all the entries
            rafs = new RAFStream(mRaf, scanOffset);
            bin = new java.io.BufferedInputStream(rafs, 4096);
            for (int i = 0; i < numEntries; i++) {
                ZipEntry newEntry = new ZipEntry(ler, bin);
                mEntries.put(newEntry.getName(), newEntry);
            }
        }

    }

    /**
        * Wrap a stream around a RandomAccessFile.  The RandomAccessFile is shared
        * among all streams returned by getInputStream(), so we have to synchronize
        * access to it.  (We can optimize this by adding buffering here to reduce
        * collisions.)
        *
        * <p>We could support mark/reset, but we don't currently need them.</p>
        */
    internal class RAFStream : java.io.InputStream {

        protected internal java.io.RandomAccessFile mSharedRaf;
        protected internal long mOffset;
        protected internal long mLength;

        public RAFStream(java.io.RandomAccessFile raf, long pos) {//throws IOException {
            mSharedRaf = raf;
            mOffset = pos;
            mLength = raf.length();
        }

        
        public override int available() {//throws IOException {
            if (mLength > mOffset) {
                if (mLength - mOffset < java.lang.Integer.MAX_VALUE) {
                    return (int)(mLength - mOffset);
                } else {
                    return java.lang.Integer.MAX_VALUE;
                }
            } else {
                return 0;
            }
        }

        
        public override int read() {//throws IOException {
            byte[] singleByteBuf = new byte[1];
            if (read(singleByteBuf, 0, 1) == 1) {
                return singleByteBuf[0] & 0XFF;
            } else {
                return -1;
            }
        }

        
        public override int read(byte[] b, int off, int len) {//throws IOException {
            lock (mSharedRaf) {
                mSharedRaf.seek(mOffset);
                if (len > mLength - mOffset) {
                    len = (int) (mLength - mOffset);
                }
                int count = mSharedRaf.read(b, off, len);
                if (count > 0) {
                    mOffset += count;
                    return count;
                } else {
                    return -1;
                }
            }
        }

        
        public override long skip(long n){// throws IOException {
            if (n > mLength - mOffset) {
                n = mLength - mOffset;
            }
            mOffset += n;
            return n;
        }
    }
    
    internal class ZipInflaterInputStream : InflaterInputStream {

        protected internal ZipEntry entry;
        protected internal long bytesRead = 0;

        public ZipInflaterInputStream(java.io.InputStream isJ, Inflater inf, int bsize, ZipEntry entry) :base(isJ, inf, bsize){
            this.entry = entry;
        }

        
        public override int read(byte[] buffer, int off, int nbytes) {//throws IOException {
            int i = base.read(buffer, off, nbytes);
            if (i != -1) {
                bytesRead += i;
            }
            return i;
        }

        
        public override int available() {//throws IOException {
            return base.available() == 0 ? 0 : (int) (entry.getSize() - bytesRead);
        }
    }
    internal class IAC_ZipEntryEnumeration : Enumeration<ZipEntry> {

        private readonly Iterator<ZipEntry> delegateInstance;
        private readonly ZipFile root;

        public IAC_ZipEntryEnumeration (Iterator<ZipEntry> delegateInstance, ZipFile root) {
            this.delegateInstance = delegateInstance;
            this.root = root;
        } 

        public bool hasMoreElements() {
            root.checkNotClosed();
            return delegateInstance.hasNext();
        }

        public ZipEntry nextElement() {
            root.checkNotClosed();
            return delegateInstance.next();
        }
}
}
