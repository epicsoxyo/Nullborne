using UnityEngine;



namespace Nullborne.GlyphCode
{

    public class GlyphsTray : MonoBehaviour
    {

        public static GlyphsTray instance;

        private Transform glyphsTransform_;
        private Transform literalsTransform_;



        private void Awake()
        {

            if(instance != null)
            {
                Debug.LogWarning("Multiple GlyphsTray instances detected!");
                return;
            }

            instance = this;

        }



        private void Start()
        {
            
            glyphsTransform_ = transform.GetChild(0);

            literalsTransform_ = transform.GetChild(1);

        }



        public void AddGlyph(GameObject glyphPrefab)
        {
            Instantiate(glyphPrefab, glyphsTransform_);
        }



        public void AddLiteral(GameObject literalPrefab)
        {
            Instantiate(literalPrefab, literalsTransform_);
        }

    }

}