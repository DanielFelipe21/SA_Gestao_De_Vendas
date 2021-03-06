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

/*

import java.nio.charset.Charset;
import java.nio.charset.IllegalCharsetNameException;
import java.security.AccessController;
import java.util.Formatter;
import java.util.IllegalFormatException;
import java.util.Locale;

import org.apache.harmony.luni.internal.nls.Messages;
import org.apache.harmony.luni.util.PriviAction;

 */
namespace biz.ritter.javapi.io
{

    /**
     * Wraps an existing {@link OutputStream} and provides convenience methods for
     * writing common data types in a human readable format. This is not to be
     * confused with DataOutputStream which is used for encoding common data types
     * so that they can be read back in. No {@code IOException} is thrown by this
     * class. Instead, callers should use {@link #checkError()} to see if a problem
     * has occurred in this stream.
     */
    public class PrintStream : FilterOutputStream, java.lang.Appendable,
            Closeable
    {

        private const String TOKEN_NULL = "null"; //$NON-NLS-1$

        /**
         * indicates whether or not this PrintStream has incurred an error.
         */
        private bool ioError;

        /**
         * indicates whether or not this PrintStream should flush its contents after
         * printing a new line.
         */
        private bool autoflush;

        private String encoding;

        private readonly String lineSeparator = java.lang.SystemJ.getProperty("line.separator"); //$NON-NLS-1$

        // private Formatter formatter;

        /**
         * Constructs a new {@code PrintStream} with {@code out} as its target
         * stream. By default, the new print stream does not automatically flush its
         * contents to the target stream when a newline is encountered.
         * 
         * @param out
         *            the target output stream.
         * @throws NullPointerException
         *             if {@code out} is {@code null}.
         */
        public PrintStream(OutputStream outJ) :
            base(outJ)
        {
            if (outJ == null)
            {
                throw new java.lang.NullPointerException();
            }
        }

        /**
         * Constructs a new {@code PrintStream} with {@code out} as its target
         * stream. The parameter {@code autoflush} determines if the print stream
         * automatically flushes its contents to the target stream when a newline is
         * encountered.
         * 
         * @param out
         *            the target output stream.
         * @param autoflush
         *            indicates whether to flush contents upon encountering a
         *            newline sequence.
         * @throws NullPointerException
         *             if {@code out} is {@code null}.
         */
        public PrintStream(OutputStream outJ, bool autoflush) :
            base(outJ)
        {
            if (outJ == null)
            {
                throw new java.lang.NullPointerException();
            }
            this.autoflush = autoflush;
        }

        /**
         * Constructs a new {@code PrintStream} with {@code out} as its target
         * stream and using the character encoding {@code enc} while writing. The
         * parameter {@code autoflush} determines if the print stream automatically
         * flushes its contents to the target stream when a newline is encountered.
         * 
         * @param out
         *            the target output stream.
         * @param autoflush
         *            indicates whether or not to flush contents upon encountering a
         *            newline sequence.
         * @param enc
         *            the non-null string describing the desired character encoding.
         * @throws NullPointerException
         *             if {@code out} or {@code enc} are {@code null}.
         * @throws UnsupportedEncodingException
         *             if the encoding specified by {@code enc} is not supported.
         */
        public PrintStream(OutputStream outJ, bool autoflush, String enc)
            ://throws UnsupportedEncodingException {
                base(outJ)
        {
            if (outJ == null || enc == null)
            {
                throw new java.lang.NullPointerException();
            }
            this.autoflush = autoflush;
            try
            {
                if (!java.nio.charset.Charset.isSupported(enc))
                {
                    throw new UnsupportedEncodingException(enc);
                }
            }
            catch (java.nio.charset.IllegalCharsetNameException e)
            {
                throw new UnsupportedEncodingException(enc);
            }
            encoding = enc;
        }

        /**
         * Constructs a new {@code PrintStream} with {@code file} as its target. The
         * virtual machine's default character set is used for character encoding.
         * 
         * @param file
         *            the target file. If the file already exists, its contents are
         *            removed, otherwise a new file is created.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         */
        public PrintStream(File file) ://throws FileNotFoundException {
            base(new FileOutputStream(file))
        {
        }

        /**
         * Constructs a new {@code PrintStream} with {@code file} as its target. The
         * character set named {@code csn} is used for character encoding.
         * 
         * @param file
         *            the target file. If the file already exists, its contents are
         *            removed, otherwise a new file is created.
         * @param csn
         *            the name of the character set used for character encoding.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws NullPointerException
         *             if {@code csn} is {@code null}.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         * @throws UnsupportedEncodingException
         *             if the encoding specified by {@code csn} is not supported.
         */
        public PrintStream(File file, String csn) ://throws FileNotFoundException,
            //UnsupportedEncodingException {
            base(new FileOutputStream(file))
        {
            if (csn == null)
            {
                throw new java.lang.NullPointerException();
            }
            if (!java.nio.charset.Charset.isSupported(csn))
            {
                throw new UnsupportedEncodingException(csn);
            }
            encoding = csn;
        }

        /**
         * Constructs a new {@code PrintStream} with the file identified by
         * {@code fileName} as its target. The virtual machine's default character
         * set is used for character encoding.
         * 
         * @param fileName
         *            the target file's name. If the file already exists, its
         *            contents are removed, otherwise a new file is created.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         */
        public PrintStream(String fileName) ://throws FileNotFoundException {
            this(new File(fileName))
        {
        }

        /**
         * Constructs a new {@code PrintStream} with the file identified by
         * {@code fileName} as its target. The character set named {@code csn} is
         * used for character encoding.
         * 
         * @param fileName
         *            the target file's name. If the file already exists, its
         *            contents are removed, otherwise a new file is created.
         * @param csn
         *            the name of the character set used for character encoding.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws NullPointerException
         *             if {@code csn} is {@code null}.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         * @throws UnsupportedEncodingException
         *             if the encoding specified by {@code csn} is not supported.
         */
        public PrintStream(String fileName, String csn) :
            //throws FileNotFoundException, UnsupportedEncodingException {
            this(new File(fileName), csn)
        {
        }

        /**
         * Flushes this stream and returns the value of the error flag.
         * 
         * @return {@code true} if either an {@code IOException} has been thrown
         *         previously or if {@code setError()} has been called;
         *         {@code false} otherwise.
         * @see #setError()
         */
        public override bool checkError()
        {
            OutputStream delegateJ = outJ;
            if (delegateJ == null)
            {
                return ioError;
            }

            flush();
            return ioError || delegateJ.checkError();
        }

        /**
         * Sets the error state of the stream to false.
         * 
         * @since 1.6
         */
        protected void clearError()
        {
            ioError = false;
        }

        /**
         * Closes this print stream. Flushes this stream and then closes the target
         * stream. If an I/O error occurs, this stream's error state is set to
         * {@code true}.
         */

        public override void close()
        {
            lock (this)
            {
                flush();
                if (outJ != null)
                {
                    try
                    {
                        outJ.close();
                        outJ = null;
                    }
                    catch (IOException e)
                    {
                        setError();
                    }
                }
            }
        }

        /**
         * Ensures that all pending data is sent out to the target stream. It also
         * flushes the target stream. If an I/O error occurs, this stream's error
         * state is set to {@code true}.
         */

        public override void flush()
        {
            lock (this)
            {
                if (outJ != null)
                {
                    try
                    {
                        outJ.flush();
                        return;
                    }
                    catch (IOException e)
                    {
                        // Ignored, fall through to setError
                    }
                }
                setError();
            }
        }

        /**
         * Writes a string formatted by an intermediate {@code Formatter} to the
         * target stream using the specified format string and arguments. For the
         * locale, the default value of the current virtual machine instance is
         * used.
         * 
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this stream.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public PrintStream format(String format, params Object[] args)
        {
            return this.format(java.util.Locale.getDefault(), format, args);
        }

        /**
         * Writes a string formatted by an intermediate {@link Formatter} to this
         * stream using the specified locale, format string and arguments.
         * 
         * @param l
         *            the locale used in the method. No localization will be applied
         *            if {@code l} is {@code null}.
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this stream.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public PrintStream format(java.util.Locale l, String format, params Object[] args)
        {
            if (format == null)
            {
                throw new java.lang.NullPointerException("format is null"); //$NON-NLS-1$
            }
            new java.util.Formatter(this, l).format(format, args);
            return this;
        }

        /**
         * Prints a formatted string. The behavior of this method is the same as
         * this stream's {@code #format(String, Object...)} method. For the locale,
         * the default value of the current virtual machine instance is used.
         * 
         * @param format
         *            the format string used for
         *            {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this stream.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public PrintStream printf(String format, params Object[] args)
        {
            return this.format(format, args);
        }

        /**
         * Prints a formatted string. The behavior of this method is the same as
         * this stream's {@code #format(Locale, String, Object...)} method.
         * 
         * @param l
         *            the locale used in the method. No localization will be applied
         *            if {@code l} is {@code null}.
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this stream.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public PrintStream printf(java.util.Locale l, String format, params Object[] args)
        {
            return this.format(l, format, args);
        }

        /**
         * Put the line separator String onto the print stream.
         */
        private void newline()
        {
            print(lineSeparator);
        }

        /**
         * Prints the string representation of the specified character array
         * to the target stream.
         * 
         * @param charArray
         *            the character array to print to the target stream.
         * @see #print(String)
         */
        public void print(char[] charArray)
        {
            print(new String(charArray, 0, charArray.Length));
        }

        /**
         * Prints the string representation of the specified character to the target
         * stream.
         * 
         * @param ch
         *            the character to print to the target stream.
         * @see #print(String)
         */
        public void print(char ch)
        {
            print(java.lang.StringJ.valueOf(ch));
        }

        /**
         * Prints the string representation of the specified double to the target
         * stream.
         * 
         * @param dnum
         *            the double value to print to the target stream.
         * @see #print(String)
         */
        public void print(double dnum)
        {
            print(java.lang.StringJ.valueOf(dnum));
        }

        /**
         * Prints the string representation of the specified float to the target
         * stream.
         * 
         * @param fnum
         *            the float value to print to the target stream.
         * @see #print(String)
         */
        public void print(float fnum)
        {
            print(java.lang.StringJ.valueOf(fnum));
        }

        /**
         * Prints the string representation of the specified integer to the target
         * stream.
         * 
         * @param inum
         *            the integer value to print to the target stream.
         * @see #print(String)
         */
        public void print(int inum)
        {
            print(java.lang.StringJ.valueOf(inum));
        }

        /**
         * Prints the string representation of the specified long to the target
         * stream.
         * 
         * @param lnum
         *            the long value to print to the target stream.
         * @see #print(String)
         */
        public void print(long lnum)
        {
            print(java.lang.StringJ.valueOf(lnum));
        }

        /**
         * Prints the string representation of the specified object to the target
         * stream.
         * 
         * @param obj
         *            the object to print to the target stream.
         * @see #print(String)
         */
        public void print(Object obj)
        {
            print(java.lang.StringJ.valueOf(obj));
        }

        /**
         * Prints a string to the target stream. The string is converted to an array
         * of bytes using the encoding chosen during the construction of this
         * stream. The bytes are then written to the target stream with
         * {@code write(int)}.
         * <p/>
         * If an I/O error occurs, this stream's error state is set to {@code true}.
         *
         * @param str
         *            the string to print to the target stream.
         * @see #write(int)
         */
        public void print(String str)
        {
            lock (this)
            {
                if (outJ == null)
                {
                    setError();
                    return;
                }
                if (str == null)
                {
                    print("null"); //$NON-NLS-1$
                    return;
                }

                try
                {
                    if (encoding == null)
                    {
                        write(str.getBytes());
                    }
                    else
                    {
                        write(str.getBytes(encoding));
                    }
                }
                catch (IOException e)
                {
                    setError();
                }
            }
        }

        /**
         * Prints the string representation of the specified boolean to the target
         * stream.
         * 
         * @param bool
         *            the boolean value to print the target stream.
         * @see #print(String)
         */
        public void print(bool boolean)
        {
            print(java.lang.StringJ.valueOf(boolean));
        }

        /**
         * Prints the string representation of the system property
         * {@code "line.separator"} to the target stream.
         */
        public void println()
        {
            newline();
        }

        /**
         * Prints the string representation of the specified character array
         * followed by the system property {@code "line.separator"} to the target
         * stream.
         * 
         * @param charArray
         *            the character array to print to the target stream.
         * @see #print(String)
         */
        public void println(char[] charArray)
        {
            println(new String(charArray, 0, charArray.Length));
        }

        /**
         * Prints the string representation of the specified character followed by
         * the system property {@code "line.separator"} to the target stream.
         * 
         * @param ch
         *            the character to print to the target stream.
         * @see #print(String)
         */
        public void println(char ch)
        {
            println(java.lang.StringJ.valueOf(ch));
        }

        /**
         * Prints the string representation of the specified double followed by the
         * system property {@code "line.separator"} to the target stream.
         * 
         * @param dnum
         *            the double value to print to the target stream.
         * @see #print(String)
         */
        public void println(double dnum)
        {
            println(java.lang.StringJ.valueOf(dnum));
        }

        /**
         * Prints the string representation of the specified float followed by the
         * system property {@code "line.separator"} to the target stream.
         * 
         * @param fnum
         *            the float value to print to the target stream.
         * @see #print(String)
         */
        public void println(float fnum)
        {
            println(java.lang.StringJ.valueOf(fnum));
        }

        /**
          * Prints the string representation of the specified integer followed by the
          * system property {@code "line.separator"} to the target stream.
          * 
          * @param inum
          *            the integer value to print to the target stream.
          * @see #print(String)
          */
        public void println(int inum)
        {
            println(java.lang.StringJ.valueOf(inum));
        }

        /**
         * Prints the string representation of the specified long followed by the
         * system property {@code "line.separator"} to the target stream.
         * 
         * @param lnum
         *            the long value to print to the target stream.
         * @see #print(String)
         */
        public void println(long lnum)
        {
            println(java.lang.StringJ.valueOf(lnum));
        }

        /**
         * Prints the string representation of the specified object followed by the
         * system property {@code "line.separator"} to the target stream.
         * 
         * @param obj
         *            the object to print to the target stream.
         * @see #print(String)
         */
        public void println(Object obj)
        {
            println(java.lang.StringJ.valueOf(obj));
        }

        /**
         * Prints a string followed by the system property {@code "line.separator"}
         * to the target stream. The string is converted to an array of bytes using
         * the encoding chosen during the construction of this stream. The bytes are
         * then written to the target stream with {@code write(int)}.
         * <p/>
         * If an I/O error occurs, this stream's error state is set to {@code true}.
         *
         * @param str
         *            the string to print to the target stream.
         * @see #write(int)
         */
        public void println(String str)
        {
            lock (this)
            {
                print(str);
                newline();
            }
        }

        /**
         * Prints the string representation of the specified boolean followed by the
         * system property {@code "line.separator"} to the target stream.
         * 
         * @param bool
         *            the boolean value to print to the target stream.
         * @see #print(String)
         */
        public void println(bool boolean)
        {
            println(java.lang.StringJ.valueOf(boolean));
        }

        /**
         * Sets the error flag of this print stream to {@code true}.
         */
        protected void setError()
        {
            ioError = true;
        }

        /**
         * Writes {@code count} bytes from {@code buffer} starting at {@code offset}
         * to the target stream. If autoflush is set, this stream gets flushed after
         * writing the buffer.
         * <p/>
         * This stream's error flag is set to {@code true} if this stream is closed
         * or an I/O error occurs.
         *
         * @param buffer
         *            the buffer to be written.
         * @param offset
         *            the index of the first byte in {@code buffer} to write.
         * @param length
         *            the number of bytes in {@code buffer} to write.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code count &lt; 0}, or if {@code
         *             offset + count} is bigger than the length of {@code buffer}.
         * @see #flush()
         */

        public override void write(byte[] buffer, int offset, int length)
        {
            // Force buffer null check first!
            if (offset > buffer.Length || offset < 0)
            {
                // luni.12=Offset out of bounds \: {0}
                throw new java.lang.ArrayIndexOutOfBoundsException("Offset out of bounds :" + offset); //$NON-NLS-1$
            }
            if (length < 0 || length > buffer.Length - offset)
            {
                // luni.18=Length out of bounds \: {0}
                throw new java.lang.ArrayIndexOutOfBoundsException("Length out of bounds :" + length); //$NON-NLS-1$
            }
            lock (this)
            {
                if (outJ == null)
                {
                    setError();
                    return;
                }
                try
                {
                    outJ.write(buffer, offset, length);
                    if (autoflush)
                    {
                        flush();
                    }
                }
                catch (IOException e)
                {
                    setError();
                }
            }
        }

        /**
         * Writes one byte to the target stream. Only the least significant byte of
         * the integer {@code oneByte} is written. This stream is flushed if
         * {@code oneByte} is equal to the character {@code '\n'} and this stream is
         * set to autoflush.
         * <p/>
         * This stream's error flag is set to {@code true} if it is closed or an I/O
         * error occurs.
         * 
         * @param oneByte
         *            the byte to be written
         */

        public override void write(int oneByte)
        {
            lock (this)
            {
                if (outJ == null)
                {
                    setError();
                    return;
                }
                try
                {
                    outJ.write(oneByte);
                    int b = oneByte & 0xFF;
                    // 0x0A is ASCII newline, 0x15 is EBCDIC newline.
                    bool isNewline = b == 0x0A || b == 0x15;
                    if (autoflush && isNewline)
                    {
                        flush();
                    }
                }
                catch (IOException e)
                {
                    setError();
                }
            }
        }

        /**
         * Appends the character {@code c} to the target stream. This method works
         * the same way as {@link #print(char)}.
         * 
         * @param c
         *            the character to append to the target stream.
         * @return this stream.
         */
        public PrintStream append(char c)
        {
            print(c);
            return this;
        }

        /**
         * Appends the character sequence {@code csq} to the target stream. This
         * method works the same way as {@code PrintStream.print(csq.toString())}.
         * If {@code csq} is {@code null}, then the string "null" is written to the
         * target stream.
         * 
         * @param csq
         *            the character sequence appended to the target stream.
         * @return this stream.
         */
        public PrintStream append(java.lang.CharSequence csq)
        {
            if (null == csq)
            {
                print(TOKEN_NULL);
            }
            else
            {
                print(csq.toString());
            }
            return this;
        }

        /**
         * Appends a subsequence of the character sequence {@code csq} to the target
         * stream. This method works the same way as {@code
         * PrintStream.print(csq.subsequence(start, end).toString())}. If {@code
         * csq} is {@code null}, then the specified subsequence of the string "null"
         * will be written to the target stream.
         * 
         * @param csq
         *            the character sequence appended to the target stream.
         * @param start
         *            the index of the first char in the character sequence appended
         *            to the target stream.
         * @param end
         *            the index of the character following the last character of the
         *            subsequence appended to the target stream.
         * @return this stream.
         * @throws IndexOutOfBoundsException
         *             if {@code start &gt; end}, {@code start &lt; 0}, {@code end &lt; 0} or
         *             either {@code start} or {@code end} are greater or equal than
         *             the length of {@code csq}.
         */
        public PrintStream append(java.lang.CharSequence csq, int start, int end)
        {
            if (null == csq)
            {
                print(TOKEN_NULL.substring(start, end));
            }
            else
            {
                print(csq.subSequence(start, end).toString());
            }
            return this;
        }
        java.lang.Appendable java.lang.Appendable.append(java.lang.CharSequence csq) { return this.append(csq); }
        java.lang.Appendable java.lang.Appendable.append(java.lang.CharSequence csq, int start, int end)
        {
            return this.append(csq, start, end);
        }
        java.lang.Appendable java.lang.Appendable.append(char c) { return this.append(c); }
    }
}