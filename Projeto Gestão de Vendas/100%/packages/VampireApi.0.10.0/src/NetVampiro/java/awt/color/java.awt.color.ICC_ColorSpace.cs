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
using org.apache.harmony.awt.gl.color;

namespace biz.ritter.javapi.awt.color
{
    /**
     * @author Oleg V. Khaschansky
     */


    [Serializable]
    public class ICC_ColorSpace : ColorSpace
    {
        private static readonly long serialVersionUID = 3455889114070431483L;

        /**
         * According to ICC specification (from http://www.color.org)
         * "For the CIEXYZ encoding, each component (X, Y, and Z)
         * is encoded as a u1Fixed15Number".
         * This means that max value for this encoding is 1 + (32767/32768)
         */
        private static readonly float MAX_XYZ = 1f + (32767f / 32768f);
        private static readonly float MAX_SHORT = 65535f;
        private static readonly float INV_MAX_SHORT = 1f / MAX_SHORT;
        private static readonly float SHORT2XYZ_FACTOR = MAX_XYZ / MAX_SHORT;
        private static readonly float XYZ2SHORT_FACTOR = MAX_SHORT / MAX_XYZ;

        private ICC_Profile profile = null;
        private float[] minValues = null;
        private float[] maxValues = null;

        // cache transforms here - performance gain
        private ICC_Transform toRGBTransform = null;
        private ICC_Transform fromRGBTransform = null;
        private ICC_Transform toXYZTransform = null;
        private ICC_Transform fromXYZTransform = null;

        private readonly ColorConverter converter = new ColorConverter();
        private readonly ColorScaler scaler = new ColorScaler();
        private bool scalingDataLoaded = false;

        private ICC_ColorSpace resolvedDeserializedInst;

        public ICC_ColorSpace(ICC_Profile pf)
            : base(pf.getColorSpaceType(), pf.getNumComponents())
        {

            int pfClass = pf.getProfileClass();

            switch (pfClass)
            {
                case ICC_Profile.CLASS_COLORSPACECONVERSION:
                case ICC_Profile.CLASS_DISPLAY:
                case ICC_Profile.CLASS_OUTPUT:
                case ICC_Profile.CLASS_INPUT:
                    break; // OK, it is color conversion profile
                default:
                    // awt.168=Invalid profile class.
                    throw new java.lang.IllegalArgumentException("Invalid profile class."); //$NON-NLS-1$
            }

            profile = pf;
            fillMinMaxValues();
        }

        public ICC_Profile getProfile()
        {
            if (profile is ICC_ProfileStub)
            {
                profile = ((ICC_ProfileStub)profile).loadProfile();
            }

            return profile;
        }

        public override float[] toRGB(float[] colorvalue)
        {
            if (toRGBTransform == null)
            {
                ICC_Profile sRGBProfile =
                    ((ICC_ColorSpace)ColorSpace.getInstance(CS_sRGB)).getProfile();
                ICC_Profile[] profiles = { getProfile(), sRGBProfile };
                toRGBTransform = new ICC_Transform(profiles);
                if (!scalingDataLoaded)
                {
                    scaler.loadScalingData(this);
                    scalingDataLoaded = true;
                }
            }

            short[] data = new short[getNumComponents()];

            scaler.scale(colorvalue, data, 0);

            short[] converted =
                converter.translateColor(toRGBTransform, data, null);

            // unscale to sRGB
            float[] res = new float[3];

            res[0] = ((converted[0] & 0xFFFF)) * INV_MAX_SHORT;
            res[1] = ((converted[1] & 0xFFFF)) * INV_MAX_SHORT;
            res[2] = ((converted[2] & 0xFFFF)) * INV_MAX_SHORT;

            return res;
        }

        public override float[] toCIEXYZ(float[] colorvalue)
        {
            if (toXYZTransform == null)
            {
                ICC_Profile xyzProfile =
                    ((ICC_ColorSpace)ColorSpace.getInstance(CS_CIEXYZ)).getProfile();
                ICC_Profile[] profiles = { getProfile(), xyzProfile };
                try
                {
                    int[] intents = {
                        ICC_Profile.icRelativeColorimetric,
                        ICC_Profile.icPerceptual};
                    toXYZTransform = new ICC_Transform(profiles, intents);
                }
                catch (CMMException e)
                { // No such tag, use what we can
                    toXYZTransform = new ICC_Transform(profiles);
                }

                if (!scalingDataLoaded)
                {
                    scaler.loadScalingData(this);
                    scalingDataLoaded = true;
                }
            }

            short[] data = new short[getNumComponents()];

            scaler.scale(colorvalue, data, 0);

            short[] converted =
                converter.translateColor(toXYZTransform, data, null);

            // unscale to XYZ
            float[] res = new float[3];

            res[0] = ((converted[0] & 0xFFFF)) * SHORT2XYZ_FACTOR;
            res[1] = ((converted[1] & 0xFFFF)) * SHORT2XYZ_FACTOR;
            res[2] = ((converted[2] & 0xFFFF)) * SHORT2XYZ_FACTOR;

            return res;
        }


        public override float[] fromRGB(float[] rgbvalue)
        {
            if (fromRGBTransform == null)
            {
                ICC_Profile sRGBProfile =
                    ((ICC_ColorSpace)ColorSpace.getInstance(CS_sRGB)).getProfile();
                ICC_Profile[] profiles = { sRGBProfile, getProfile() };
                fromRGBTransform = new ICC_Transform(profiles);
                if (!scalingDataLoaded)
                {
                    scaler.loadScalingData(this);
                    scalingDataLoaded = true;
                }
            }

            // scale rgb value to short
            short[] scaledRGBValue = new short[3];
            scaledRGBValue[0] = (short)(rgbvalue[0] * MAX_SHORT + 0.5f);
            scaledRGBValue[1] = (short)(rgbvalue[1] * MAX_SHORT + 0.5f);
            scaledRGBValue[2] = (short)(rgbvalue[2] * MAX_SHORT + 0.5f);

            short[] converted =
                converter.translateColor(fromRGBTransform, scaledRGBValue, null);

            float[] res = new float[getNumComponents()];

            scaler.unscale(res, converted, 0);

            return res;
        }


        public override float[] fromCIEXYZ(float[] xyzvalue)
        {
            if (fromXYZTransform == null)
            {
                ICC_Profile xyzProfile =
                    ((ICC_ColorSpace)ColorSpace.getInstance(CS_CIEXYZ)).getProfile();
                ICC_Profile[] profiles = { xyzProfile, getProfile() };
                try
                {
                    int[] intents = {
                        ICC_Profile.icPerceptual,
                        ICC_Profile.icRelativeColorimetric};
                    fromXYZTransform = new ICC_Transform(profiles, intents);
                }
                catch (CMMException e)
                { // No such tag, use what we can
                    fromXYZTransform = new ICC_Transform(profiles);
                }

                if (!scalingDataLoaded)
                {
                    scaler.loadScalingData(this);
                    scalingDataLoaded = true;
                }
            }

            // scale xyz value to short
            short[] scaledXYZValue = new short[3];
            scaledXYZValue[0] = (short)(xyzvalue[0] * XYZ2SHORT_FACTOR + 0.5f);
            scaledXYZValue[1] = (short)(xyzvalue[1] * XYZ2SHORT_FACTOR + 0.5f);
            scaledXYZValue[2] = (short)(xyzvalue[2] * XYZ2SHORT_FACTOR + 0.5f);

            short[] converted =
                converter.translateColor(fromXYZTransform, scaledXYZValue, null);

            float[] res = new float[getNumComponents()];

            scaler.unscale(res, converted, 0);

            return res;
        }


        public override float getMinValue(int component)
        {
            if ((component < 0) || (component > this.getNumComponents() - 1))
            {
                // awt.169=Component index out of range
                throw new java.lang.IllegalArgumentException("Component index out of range"); //$NON-NLS-1$
            }

            return minValues[component];
        }


        public override float getMaxValue(int component)
        {
            if ((component < 0) || (component > this.getNumComponents() - 1))
            {
                // awt.169=Component index out of range
                throw new java.lang.IllegalArgumentException("Component index out of range"); //$NON-NLS-1$
            }

            return maxValues[component];
        }

        private void fillMinMaxValues()
        {
            int n = getNumComponents();
            maxValues = new float[n];
            minValues = new float[n];
            switch (getType())
            {
                case ColorSpace.TYPE_XYZ:
                    minValues[0] = 0;
                    minValues[1] = 0;
                    minValues[2] = 0;
                    maxValues[0] = MAX_XYZ;
                    maxValues[1] = MAX_XYZ;
                    maxValues[2] = MAX_XYZ;
                    break;
                case ColorSpace.TYPE_Lab:
                    minValues[0] = 0;
                    minValues[1] = -128;
                    minValues[2] = -128;
                    maxValues[0] = 100;
                    maxValues[1] = 127;
                    maxValues[2] = 127;
                    break;
                default:
                    for (int i = 0; i < n; i++)
                    {
                        minValues[i] = 0;
                        maxValues[i] = 1;
                    }
                    break;
            }
        }
    }

}