using EasyConfig.Utils;

namespace EasyConfig.Types {
    public abstract class GameConfig : IBaseConfig {
        // Name of config file. Defaults to the derived class' name.
        public string ConfigName { get; }
        
        /// <summary>
        /// Saves the config with the configured serializer.
        /// </summary>
        public virtual void Save() {
            this.SaveConfig();
        }
        
        /// <summary>
        /// Loads config for associated config. Don't use pure base implementation.
        /// See examples for proper implementation.
        /// <example>public void Load() =&gt; base.Load&lt;NetworkConfig&gt;(this);</example>
        /// </summary>
        /// <param name="config">Config to populate</param>
        /// <typeparam name="T">Config type used for deserialization</typeparam>
        public virtual void Load<T>(GameConfig config) where T : GameConfig {
           var c = ConfigUtility.LoadConfig<T>(config.GetConfigPath());
           ConfigUtility.ApplyLoad(c, config);
        }

        protected GameConfig() {
            ConfigName = this.GetType().Name;
        }
    }
}