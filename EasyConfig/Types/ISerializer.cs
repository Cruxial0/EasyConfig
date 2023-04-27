namespace EasyConfig.Types {
    public interface ISerializer {
        public SerializeFormat Format { get; }
        public void Save(EasyConfig config);
        /// <summary>
        /// Load method for serializers. Must always return a value.
        /// Do not use this method directly. It is only to be used via overrides.
        /// </summary>
        /// <param name="config"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>(string path) where T : EasyConfig;
    }
}