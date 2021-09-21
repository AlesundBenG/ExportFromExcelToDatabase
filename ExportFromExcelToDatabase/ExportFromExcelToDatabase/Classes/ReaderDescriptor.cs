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

        /// <param name="objectsPart">Pattert part: <tag>...</tag></param>
        private DescriptorObject getDescriptorObject(string objectsPart) {
            DescriptorObject descriptor = new DescriptorObject();
            descriptor.NameObject = objectsPart.Substring(1, objectsPart.IndexOf('>') - 1);
            List<string> tokens = splitObjectsPartOnToken(objectsPart.Replace($"<{descriptor.NameObject}>", "").Replace($"</{descriptor.NameObject}>", ""));
            for (int i = 0; i < tokens.Count; i++) {
                descriptor.addToken(getToken(tokens[i]));
            }
            return descriptor;
        }

        private Token getToken(string pathToken) {
            int startPosition = getPositionAfterSpace(pathToken, 0);
            int endPosition = getPositionNextSpace(pathToken, startPosition) - 1;
            string name = pathToken.Substring(startPosition, endPosition - startPosition - 1);
            string value = getStringBetweenQuotation(pathToken, endPosition);
            return new Token() {
                Name = name,
                Value = value
            };
        }

        private List<string> splitObjectsPartOnToken(string objectsPath) {
            List<string> listPart = new List<string>();
            string part = "";
            int startPosition = 0;
            int currentPosition = 0;
            bool betweenQuotation = false;
            while (currentPosition < objectsPath.Length) {
                if (objectsPath[currentPosition] == '\"') {
                    betweenQuotation = !betweenQuotation;
                }
                else if ((objectsPath[currentPosition] == ';') && (!betweenQuotation)) {
                    part = objectsPath.Substring(startPosition, currentPosition - startPosition + 1);
                    listPart.Add(part);
                    startPosition = currentPosition + 1;
                }
                currentPosition++;
            }
            part = objectsPath.Substring(startPosition, currentPosition - startPosition);
            if (part.Replace('\t', ' ').Replace(" ", "").Length > 0) {
                listPart.Add(part);
            }
            return listPart;
        }

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

        private int getPositionNextSpace(string text, int startPosition) {
            int currentPosition = startPosition;
            while (currentPosition < text.Length) {
                if ((text[currentPosition] == ' ') || (text[currentPosition] == '\t')) {
                    return currentPosition;
                }
                else {
                    currentPosition++;
                }
            }
            return -1;
        }

        private string getStringBetweenQuotation(string text, int startPosition) {
            int currentPosition = startPosition;
            int firstQuotationPosition = currentPosition;
            bool foundFirst = false;
            while (currentPosition < text.Length) {
                if (text[currentPosition] == '\"')  {
                    if ((text[currentPosition - 1] != '\\') && (!foundFirst)) {
                        foundFirst = true;
                        firstQuotationPosition = currentPosition;
                    }
                    else if ((text[currentPosition - 1] != '\\') && (foundFirst)) {
                        return text.Substring(firstQuotationPosition + 1, currentPosition - firstQuotationPosition - 1);
                    }
                }
                currentPosition++;
            }
            if (foundFirst) {
                throw new Exception($"Not found second quotation: \"{text.Substring(firstQuotationPosition, text.Length - firstQuotationPosition)}");
            }
            else {
                throw new Exception($"Not found first quotation: \"{text.Substring(startPosition, text.Length - startPosition)}");
            }
        }
    }
}
