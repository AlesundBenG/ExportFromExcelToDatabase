using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportFromExcelToDatabase.Classes
{
    public class ReaderDescriptor
    {

        public List<DescriptorObject> read(string descriptor) {
            List<DescriptorObject> listDescriptor = new List<DescriptorObject>();
            List<string> objectsPart = splitDescriptorOnObjectsPart(descriptor);
            for (int i = 0; i < objectsPart.Count; i++) {
                listDescriptor.Add(getDescriptorObject(objectsPart[i]));
            }
            return listDescriptor;
        }

        /// <summary>
        /// Pattert part: <tag>...</tag>
        /// </summary>
        private List<string> splitDescriptorOnObjectsPart(string descriptor) {
            List<string> listPart = new List<string>();
            string tag;
            int startPosition = 0;
            int endTag = 0;
            int endPosition = 0;
            while (startPosition < descriptor.Length) {
                startPosition = getPositionAfterSpace(descriptor, startPosition);
                if (startPosition == -1) {
                    break;
                }
                //Read tag.
                if (descriptor[startPosition] != '<') {
                    throw new Exception($"ReaderDescriptor: the \"<\" was expected but was found {descriptor[startPosition]} (position {startPosition})");
                }
                endTag = descriptor.IndexOf('>', startPosition);
                if (endTag == -1) {
                    throw new Exception($"ReaderDescriptor: not found the \">\"");
                }
                else {
                    tag = descriptor.Substring(startPosition + 1, endTag - startPosition - 1);
                }
                //Search end object's part.
                endPosition = descriptor.IndexOf($"</{tag}>", endTag + 1) + tag.Length + 3;
                if (endPosition == -1) {
                    throw new Exception($"ReaderDescriptor: not found the <\\{tag}>");
                }
                else {
                    listPart.Add(descriptor.Substring(startPosition, endPosition - startPosition));
                    startPosition = endPosition;
                }
            }
            return listPart;
        }


        /// <param name="objectsPart">Pattert part: <tag>...</tag></param>
        private DescriptorObject getDescriptorObject(string objectsPart) {
            DescriptorObject descriptor = new DescriptorObject();
            descriptor.NameObject = objectsPart.Substring(1, objectsPart.IndexOf('>') - 1);
            return descriptor;
        }

        private int getPositionAfterSpace(string text, int startPosition) {
            int currentPosition = startPosition;
            while (currentPosition < text.Length) {
                if ((text[currentPosition] == ' ') || (text[currentPosition] == '\t')) {
                    currentPosition++;
                }
                else {
                    return currentPosition;
                }
            }
            return -1;
        }
    }
}
