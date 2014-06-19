$: << File.dirname(__FILE__)

class Generator  
  def self.generate
    generator = Generator.new
    generator.generate_csproj
    generator.generate_messages
  end
end